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
            btnStart.Text = "��ʼ����";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.Description = "��ѡ��һ���ļ���";

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                tbSelectedPath.Text = folderDialog.SelectedPath;
                // ʹ��selectedPath
                btnStart.Enabled = true;
            }
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            // ��ȡ������Ϣ
            if (!int.TryParse(input_quality.Text, out int quality))
            {
                MessageBox.Show("���������룺1 ~ 100 ������");
                return;
            }
            if (quality < 1 || quality > 100)
            {
                MessageBox.Show("���������룺1 ~ 100 ������");
                return;
            }

            bool isResize = radioButton1.Checked;
            decimal sizeWidth = 0m;
            decimal sizeHeight = 0m;
            if (isResize)
            {
                if (!decimal.TryParse(input_resize_width.Text, out sizeWidth))
                {
                    MessageBox.Show("��������������");
                    return;
                }
                else if (sizeWidth < 0)
                {
                    MessageBox.Show("��������������");
                    return;
                }

                if (!decimal.TryParse(input_resize_height.Text, out sizeHeight))
                {
                    MessageBox.Show("�ߣ�������������");
                    return;
                }
                else if (sizeWidth < 0)
                {
                    MessageBox.Show("�ߣ�������������");
                    return;
                }
            }


            btnStart.Enabled = false;
            btnStart.Text = "������...";
            progressBar_compress.Value = 0;

            string directoryPath = tbSelectedPath.Text;

            // ��ȡ�����ļ���������Ŀ¼�е��ļ�
            var files = Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories).ToList();

            var outpath = Path.Combine(Path.GetTempPath(), "RESULT FILE");
            if (!Directory.Exists(outpath))
            {
                // Ŀ¼�����ڣ�����Ŀ¼
                Directory.CreateDirectory(outpath);
            }
            ManualResetEvent completionEvent = new ManualResetEvent(false);
            int count = files.Count;
            int totalCount = 0;
            int successcount = 0;
            int errorcount = 0;
            var exceptionmessage = new StringBuilder();

            txtResultInfo.AppendText($"---- ���� {count} ���ļ���Ҫ���� {DateTime.Now:yyyy/MM/dd HH:mm:ss} ----");

            // ����ͼƬ��׺������
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
                                txtResultInfo.AppendText($"\r\n���ȣ�{totalCount} / {files.Count}  ��{filename}�� ����ʽ��֧��ѹ������������");
                            }
                            return;
                        }

                        var outFilePath = Path.Combine(outpath, filename);

                        using (var image = SixLabors.ImageSharp.Image.Load(file))
                        {
                            // ��������ֱ��ʣ��������ֱ���ȥ�����ߴ�
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
                                // gif ���⴦��
                                await image.SaveAsGifAsync(outFilePath, new GifEncoder
                                {
                                    Quantizer = KnownQuantizers.Wu
                                });
                            }
                            else
                            {
                                // ������̬�ļ����洢Ϊjpg��ʽ
                                await image.SaveAsJpegAsync(outFilePath, new JpegEncoder
                                {
                                    Quality = quality
                                });
                            }
                        }

                        // �Ƚ�ѹ�����ͼƬ �� δѹ��ͼƬ�Ĵ�С�����С��Դ�ļ���������ƶ����ǲ���������ֱ��ɾ��Դ�ļ�
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
                            txtResultInfo.AppendText($"\r\n���ȣ�{totalCount} / {files.Count}  ��{filename}�� ���ɹ���");
                        }
                    }
                    catch (Exception ex)
                    {
                        lock (this)
                        {
                            Interlocked.Increment(ref totalCount);
                            Interlocked.Increment(ref errorcount);
                            progressBar_compress.Value = (int)(totalCount * 100m / files.Count);
                            txtResultInfo.AppendText($"\r\n���ȣ�{totalCount} / {files.Count}  ��{filename}�� ���쳣��{ex.Message}��");
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

            // �ȴ�����Ԫ�ش������
            completionEvent.WaitOne();

            txtResultInfo.AppendText($"\r\n---- ������� {DateTime.Now:yyyy/MM/dd HH:mm:ss} ----\r\n�ܼƣ�{files.Count} ��, ������ɣ�{successcount} ��������ʧ�ܣ�{errorcount} ��\r\n\r\n");

            btnStart.Enabled = true;
            btnStart.Text = "��ʼ����";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            // ���ÿ�����ݿ�
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
            // ���ÿ�����ݿ�
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
