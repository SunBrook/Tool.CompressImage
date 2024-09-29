using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp;
using System.Text;

namespace Tool.CompressImage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            btnStart.Enabled = false;
            btnStart.Text = "开始处理";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.Description = "请选择一个文件夹";

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                tbSelectedPath.Text = folderDialog.SelectedPath;
                // 使用selectedPath
                btnStart.Enabled = true;
            }
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            // 获取设置信息
            if (!int.TryParse(input_quality.Text, out int quality))
            {
                MessageBox.Show("质量请输入：1 ~ 100 的整数");
                return;
            }
            if (quality < 1 || quality > 100)
            {
                MessageBox.Show("质量请输入：1 ~ 100 的整数");
                return;
            }

            bool isResize = radioButton1.Checked;
            decimal sizeWidth = 0m;
            decimal sizeHeight = 0m;
            if (isResize)
            {
                if (!decimal.TryParse(input_resize_width.Text, out sizeWidth))
                {
                    MessageBox.Show("宽：请输入正整数");
                    return;
                }
                else if (sizeWidth < 0)
                {
                    MessageBox.Show("宽：请输入正整数");
                    return;
                }

                if (!decimal.TryParse(input_resize_height.Text, out sizeHeight))
                {
                    MessageBox.Show("高：请输入正整数");
                    return;
                }
                else if (sizeWidth < 0)
                {
                    MessageBox.Show("高：请输入正整数");
                    return;
                }
            }


            btnStart.Enabled = false;
            btnStart.Text = "处理中...";
            progressBar_compress.Value = 0;

            string directoryPath = tbSelectedPath.Text;

            // 获取所有文件，包括子目录中的文件
            var files = Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories).ToList();

            var outpath = Path.Combine(Path.GetTempPath(), "RESULT FILE");
            if (!Directory.Exists(outpath))
            {
                // 目录不存在，创建目录
                Directory.CreateDirectory(outpath);
            }
            ManualResetEvent completionEvent = new ManualResetEvent(false);
            int count = files.Count;
            int totalCount = 0;
            int successcount = 0;
            int errorcount = 0;
            var exceptionmessage = new StringBuilder();

            txtResultInfo.AppendText($"---- 共有 {count} 个文件需要处理 {DateTime.Now:yyyy/MM/dd HH:mm:ss} ----");

            // 常用图片后缀名过滤
            var picSuffixs = new string[] { ".bmp", ".gif", ".jpeg", ".jpg", ".ppm", ".png", ".tif", ".tiff", ".tga", ".webp" };

            await Task.Run(() =>
            {
                Parallel.ForEach(files, new ParallelOptions { MaxDegreeOfParallelism = 100 }, async file =>
                {
                    var filename = Path.GetFileName(file);

                    try
                    {
                        var suffix = Path.GetExtension(file);
                        if (!picSuffixs.Contains(suffix))
                        {
                            lock (this)
                            {
                                Interlocked.Increment(ref totalCount);
                                Interlocked.Increment(ref errorcount);
                                progressBar_compress.Value = (int)(totalCount * 100m / files.Count);
                                txtResultInfo.AppendText($"\r\n进度：{totalCount} / {files.Count}  “{filename}” 【格式不支持压缩，已跳过】");
                            }
                            return;
                        }

                        var outFilePath = Path.Combine(outpath, filename);

                        using (var image = SixLabors.ImageSharp.Image.Load(file))
                        {
                            // 超出桌面分辨率，按照最大分辨率去调整尺寸
                            if (isResize)
                            {
                                if (image.Width > sizeWidth || image.Height > sizeHeight)
                                {
                                    var widthRate = Math.Round(sizeWidth / image.Width, 2);
                                    var heightRate = Math.Round(sizeHeight / image.Width, 2);
                                    var resizeRate = widthRate > heightRate ? widthRate : heightRate;
                                    image.Mutate(x => x.Resize((int)(image.Width * resizeRate), (int)(image.Height * resizeRate)));
                                }
                            }

                            if (suffix == ".gif")
                            {
                                // gif 特殊处理
                                await image.SaveAsGifAsync(outFilePath, new GifEncoder
                                {
                                    Quantizer = KnownQuantizers.Wu
                                });
                            }
                            else
                            {
                                // 其他静态文件，存储为jpg格式
                                await image.SaveAsJpegAsync(outFilePath, new JpegEncoder
                                {
                                    Quality = quality
                                });
                            }
                        }

                        // 比较压缩完的图片 和 未压缩图片的大小，如果小于源文件，则进行移动覆盖操作，否则直接删除源文件
                        var outPathFile = new FileInfo(outFilePath);
                        var originalPathFile = new FileInfo(file);
                        if (outPathFile.Length < originalPathFile.Length)
                        {
                            outPathFile.MoveTo(file, overwrite: true);
                        }
                        else
                        {
                            outPathFile.Delete();
                        }

                        lock (this)
                        {
                            Interlocked.Increment(ref totalCount);
                            Interlocked.Increment(ref successcount);
                            progressBar_compress.Value = (int)(totalCount * 100m / files.Count);
                            txtResultInfo.AppendText($"\r\n进度：{totalCount} / {files.Count}  “{filename}” 【成功】");
                        }
                    }
                    catch (Exception ex)
                    {
                        lock (this)
                        {
                            Interlocked.Increment(ref totalCount);
                            Interlocked.Increment(ref errorcount);
                            progressBar_compress.Value = (int)(totalCount * 100m / files.Count);
                            txtResultInfo.AppendText($"\r\n进度：{totalCount} / {files.Count}  “{filename}” 【异常：{ex.Message}】");
                        }
                    }
                    finally
                    {
                        if (Interlocked.Decrement(ref count) == 0)
                        {
                            completionEvent.Set();
                        }
                    }
                });
            });

            // 等待所有元素处理完成
            completionEvent.WaitOne();

            txtResultInfo.AppendText($"\r\n---- 处理完成 {DateTime.Now:yyyy/MM/dd HH:mm:ss} ----\r\n总计：{files.Count} 个, 处理完成：{successcount} 个，处理失败：{errorcount} 个\r\n\r\n");

            btnStart.Enabled = true;
            btnStart.Text = "开始处理";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            // 启用宽高数据框
            if (!input_resize_width.Enabled)
            {
                input_resize_width.Enabled = true;
                label5.Enabled = true;
            }
            if (!input_resize_height.Enabled)
            {
                input_resize_height.Enabled = true;
                label6.Enabled = true;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            // 禁用宽高数据框
            if (input_resize_width.Enabled)
            {
                input_resize_width.Enabled = false;
                label5.Enabled = false;
            }
            if (input_resize_height.Enabled)
            {
                input_resize_height.Enabled = false;
                label6.Enabled = false;
            }
        }

        private void btn_set_reset_Click(object sender, EventArgs e)
        {
            input_quality.Text = "75";
            radioButton1.Checked = true;
            input_resize_width.Enabled = true;
            label5.Enabled = true;
            input_resize_height.Enabled = true;
            label6.Enabled = true;
            input_resize_width.Text = "1920";
            input_resize_height.Text = "1080";
        }
    }
}
