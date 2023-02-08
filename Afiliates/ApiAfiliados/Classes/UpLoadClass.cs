using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
//using System.Drawing;
//using System.Drawing.Drawing2D;
//using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats;

namespace ApiAfiliados.Classes
{

        public class UpLoadImage
        {
            private readonly IWebHostEnvironment _env;
            string webRootPath;
            string webRootPathLog;
            public UpLoadImage(IWebHostEnvironment env)
            {
                _env = env ?? throw new ArgumentNullException(nameof(env));
                //PROD
                webRootPath = _env.WebRootPath;
                webRootPathLog = _env.WebRootPath;
                //DEV
                // webRootPath = _env.WebRootPath + "..\\..\\..\\cdn";
            }

            /// <summary>
            /// Devolve o caminho da imagem
            /// </summary>
            /// <param name="url"></param>
            /// <param name="seller"></param>
            /// <param name="document"></param>
            /// <returns></returns>
            public string ReturnImage(string url, string seller, string document)
            {
                seller = seller.Replace("-", "").Replace(".", "").Replace("//", "");

                var file = url + "Documents" + "/" + seller + "/" + document;

                return file;
            }

            /// <summary>
            /// Remove a imagem
            /// </summary>
            /// <param name="Paths"></param>
            /// <param name="oldImg"></param>
            /// <returns></returns>
            public bool remove(string Paths, string olddocument)
            {
                // pasta fisica do servidor


                var uploads = Path.Combine(webRootPath, Paths);

                if (System.IO.File.Exists(Path.Combine(uploads, olddocument)))
                {
                    try
                    {
                        using (var fileStream = new FileStream(Path.Combine(uploads, olddocument), FileMode.Open))
                        {

                            fileStream.Close();
                            System.IO.File.Delete(Path.Combine(uploads, olddocument));

                            return true;

                        }

                    }
                    catch { return false; }
                }
                else
                {
                    return true;
                }

                return false;
            }

            /// <summary>
            /// UPLOAD IMAGE
            /// </summary>
            /// <param name="files">Arquivo vindo do action</param>
            /// <param name="fileNames">Nome final do arquivo</param>
            /// <param name="Paths">Caminho onde será salvo o arquivo</param>
            /// <param name="oldImg">Arquivo anterior a este - ele será removido</param>
            /// <param name="retWhite">Usar a imagem quadrada, add um quadrado branco no fundo</param>
            /// <returns></returns>
            public bool UploadUpLoad(Microsoft.AspNetCore.Http.IFormFile files, string Paths, string fileNames, string olddocument = null, bool retWhite = false, bool thumbs = false, int width = 600)
            {
                string error = null;
                //webRootPath = _env.WebRootPath; // pasta fisica do servidor
                string[] extenion = files.FileName.Split('.');

                var uploads = Path.Combine(webRootPath, Paths);
                Directory.CreateDirectory(uploads);


                error = "files-001";

                if (olddocument != null)
                {
                    error = "files-002";
                    try
                    {
                        using (var fileStream = new FileStream(Path.Combine(uploads, olddocument), FileMode.Open))
                        {
                            if (System.IO.File.Exists(Path.Combine(uploads, olddocument)))
                            {
                                error = "files-003";
                                fileStream.Close();
                                System.IO.File.Delete(Path.Combine(uploads, olddocument));
                            }
                        }
                    }
                    catch { }
                }
            try
            {
                error = "files-004";

                string _files = fileNames.Split('.').ToList().LastOrDefault();
                _files = fileNames.Replace("." + _files, "");
                Console.WriteLine(_files);
                // System.IO.File.Delete(_files);

                string[] fileslist = Directory.GetFiles(uploads, _files + ".*");
                Console.WriteLine(fileslist.Count());
                foreach (var item in fileslist)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine(fileslist);
                foreach (string file in fileslist)
                {
                    Console.WriteLine(file);
                    try
                    {

                        File.Delete(file);
                    }
                    catch (Exception ex2)
                    {
                        Console.WriteLine(ex2.Message);
                        Console.WriteLine(ex2.InnerException.Message);
                    };

                }
            }
            catch(Exception ex3) {
                Console.WriteLine(ex3.Message);
                Console.WriteLine(ex3.InnerException.Message);
            }

            try
            {
                IImageFormat format;
                error = "files-006";
                using (var fileStream = SixLabors.ImageSharp.Image.Load(files.OpenReadStream(), out format))
                {
                    string fileName = Path.Combine(uploads, fileNames);

                    int size = width;

                    error = "files-007";

                    int newImageHeight = (int)(fileStream.Height * ((float)size / (float)fileStream.Width));
                    fileStream.Mutate(x => x.Resize(size, newImageHeight));

                  

                    error = "files-008";
                    //verificar tamanho da imagem

                    double dblWidth_origial = width; //largura do sistema

                    double dblHeigth_origial = fileStream.Height;//altura orginal

                    double relation_heigth_width = dblHeigth_origial / dblWidth_origial;
                    double relation_width_heigth = dblWidth_origial / dblHeigth_origial;

                    int newheigth = (relation_heigth_width > relation_width_heigth) ? size : (int)(size * relation_heigth_width);
                    int newwidth = (relation_heigth_width < relation_width_heigth) ? size : (int)(size * relation_width_heigth);

                    //int new_Height = (int)(size * relation_heigth_width);

                    try
                    { 
                        fileStream.Save(fileName);
                        error = "files-009";
                       
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.InnerException.Message);
                        return false;
                    }


                }


                //thumbs
                if (thumbs)
                {
                    
                    error = "files-006";
                    using (var fileStream = SixLabors.ImageSharp.Image.Load(files.OpenReadStream(), out format))
                    {
                        string fileName = Path.Combine(uploads, "thumbs_", fileNames);

                        int size = 72;

                        error = "files-007";

                        int newImageHeight = (int)(fileStream.Height * ((float)size / (float)fileStream.Width));
                        fileStream.Mutate(x => x.Resize(size, newImageHeight));



                        error = "files-008";
                        //verificar tamanho da imagem

                        double dblWidth_origial = width; //largura do sistema

                        double dblHeigth_origial = fileStream.Height;//altura orginal

                        double relation_heigth_width = dblHeigth_origial / dblWidth_origial;
                        double relation_width_heigth = dblWidth_origial / dblHeigth_origial;

                        int newheigth = (relation_heigth_width > relation_width_heigth) ? size : (int)(size * relation_heigth_width);
                        int newwidth = (relation_heigth_width < relation_width_heigth) ? size : (int)(size * relation_width_heigth);

                        //int new_Height = (int)(size * relation_heigth_width);

                        try
                        {
                            fileStream.Save(fileName);
                            error = "files-009";

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine(ex.InnerException.Message);
                           // return false;
                        }


                    }
                }

                    return true;
                    
                }
                catch (Exception ex)
                {
                    Console.Write(String.Format("Erro :{0} , {1}", error, ex.Message));
                    return false;
                }
                return false;
            }

        public bool UploadUpLoad2(Microsoft.AspNetCore.Http.IFormFile files, string Paths, string fileNames, int size, string oldImg = null, bool retWhite = false)
        {

            //webRootPath = _env.WebRootPath; // pasta fisica do servidor
            
            //string[] extenion = files.FileName.Split('.');

            //var uploads = Path.Combine(webRootPath, Paths);
            //Directory.CreateDirectory(uploads);


            //if (oldImg != null)
            //{
            //    try
            //    {
            //        using (var fileStream = new FileStream(Path.Combine(uploads, oldImg), FileMode.Open))
            //        {
            //            if (System.IO.File.Exists(Path.Combine(uploads, oldImg)))
            //            {
            //                fileStream.Close();
            //                System.IO.File.Delete(Path.Combine(uploads, oldImg));
            //            }
            //        }
            //    }
            //    catch { }
            //}
            //try
            //{
            //    using (var fileStream = new FileStream(Path.Combine(uploads, fileNames), FileMode.Open))
            //    {
            //        if (System.IO.File.Exists(Path.Combine(uploads, fileNames)))
            //        {
            //            fileStream.Close();
            //            System.IO.File.Delete(Path.Combine(uploads, fileNames));

            //        }
            //    }
            //}
            //catch { }

            //try
            //{

            //    using (var fileStream = new FileStream(Path.Combine(uploads, fileNames), FileMode.Create))
            //    {
            //        System.Drawing.Image obj;
            //        Bitmap newImg;
            //        obj = System.Drawing.Image.FromStream(files.OpenReadStream());


            //        //verificar tamanho da imagem

            //        double dblWidth_origial = obj.Width; //largura original

            //        double dblHeigth_origial = obj.Height;//altura orginal

            //        double relation_heigth_width = dblHeigth_origial / dblWidth_origial;
            //        double relation_width_heigth = dblWidth_origial / dblHeigth_origial;

            //        int newheigth = (relation_heigth_width > relation_width_heigth) ? size : (int)(size * relation_heigth_width);
            //        int newwidth = (relation_heigth_width < relation_width_heigth) ? size : (int)(size * relation_width_heigth);

            //        //int new_Height = (int)(size * relation_heigth_width);



            //        ImageFormat imageFormat = obj.RawFormat;

            //        newImg = new Bitmap(obj, newwidth, newheigth);

            //        string fileName = Path.Combine(uploads, fileNames);
            //        fileStream.Close();
            //        fileStream.Dispose();

            //        // newImg.Save(fileName, imageFormat);

            //        if (retWhite)
            //        {

            //            var filePathWater = "images\\bg_white.png";
            //            var watermarkIMG = Path.Combine(webRootPath, filePathWater);
            //            System.Drawing.Image objBG;
            //            objBG = System.Drawing.Image.FromFile(watermarkIMG);

            //            Bitmap newImgBG = new Bitmap(objBG, size, size);


            //            Bitmap img3 = new Bitmap(size, size);
            //            int pSizeX = (newImg.Width < size) ? ((size - newImg.Width) / 2) : 0;
            //            int pSizeY = (newImg.Height < size) ? ((size - newImg.Height) / 2) : 0;



            //            Graphics g = Graphics.FromImage(img3);



            //            g.Clear(System.Drawing.Color.Black);
            //            g.Clear(System.Drawing.Color.White);




            //            g.DrawImage(newImgBG, new System.Drawing.Point(newImg.Width, 0));
            //            g.DrawImage(newImg, new System.Drawing.Point(pSizeX, pSizeY));
            //            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            //            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            //            g.Dispose();
            //            obj.Dispose();
            //            objBG.Dispose();

            //            img3.Save(fileName, imageFormat);
            //            img3.Dispose();

            //        }

            //        fileStream.Close();
            //        fileStream.Dispose();

            //        return true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.Write(ex.Message);
            //    return false;
            //}
            return false;
        }
        //return img to Base64 mais lento
        private string returnImg(string brand, string id_seller)
            {
                string img = null;

                if (brand != null && brand != "")
                {
                    string patch = (Regex.Replace(id_seller, @"[^a-zA-Z0-9_]+", "")).ToString().ToLower().Replace(" ", "");
                    var filePath = "Documents\\" + patch + "\\";
                    string[] extension = brand.Split('.');
                    var uploads = Path.Combine(webRootPath, filePath);


                    using (var fileStream = new FileStream(Path.Combine(uploads, brand), FileMode.Open))
                    {
                        if (System.IO.File.Exists(Path.Combine(uploads, brand)))
                        {

                            fileStream.Close();

                            byte[] imageArray = System.IO.File.ReadAllBytes(uploads + brand);
                            img = "data:image/" + extension.Last() + ";base64," + Convert.ToBase64String(imageArray);
                        }
                    }
                }


                return img;
            }

            private string EncoderText(string text)
            {
                byte[] bytes = Encoding.Default.GetBytes(text);
                text = Encoding.UTF8.GetString(bytes);

                return text;

            }
        }
    }