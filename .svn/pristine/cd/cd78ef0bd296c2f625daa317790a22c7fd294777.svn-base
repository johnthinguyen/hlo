using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.IO;
using WebResxLanguage.Models;
using System.Xml.Linq;
using System.Web;
using System.Data;
using ClosedXML.Excel;
using System.Xml;
using ExcelDataReader;
using Newtonsoft.Json;
using MyUtility;
using System.Web.UI;

namespace WebResxLanguage.Controllers
{
    public class HomeController : Controller
    {
        public static int folderId = 0;
        public static string selectLanguage = "";
        public static string selectLanguageImport = "";
        public static List<string> langDefaultCheck = null;
        public List<string> listXmlNotFile = null;
        public static List<string> ListNewKey = null;
        public static List<string> listLangCookie = null;
        public static bool changeFolderTemp = false;

        [HttpGet]
        public ActionResult Index(string searchString = null, int folderID = 0, string ddlLanguage = "En", string ddlLanguageImport = "")
        {
            var query = new List<ElementV2Model>();
            var query1 = new List<ElementV2Model>();
            var query_list = new List<List<ElementV2Model>>();
            ViewBag.ListItV3 = query_list;
            //Lấy ngôn ngữ được chọn export
            selectLanguage = ddlLanguage;

            //path = D:/ResxLanguagesUtility/ResxLanguages/
            var path = System.Configuration.ConfigurationManager.AppSettings["UrlFolder"];
            var strLangs = System.Configuration.ConfigurationManager.AppSettings["Langs"];

            //Lấy list path folder theo path
            string[] list_path_folder = Directory.GetDirectories(path);

            //Tạo mới 1 List FolderModel với thuộc tính là ID , NAME
            var list_folder = new List<FolderModel>();
            FolderModel folder = null;

            ViewBag.SearchString = searchString;

            //Duyệt mảng list_path_folder
            //Ở từng list_path_folder ta thay thế tất cả ký tự trùng với biến path thành ""
            //Rồi sau đó add vào mảng list_folder
            //vd 1 list_path_folder = D:/ResxLanguagesUtility/ResxLanguages/Account
            // Sau khi thay thế path = "D:/ResxLanguagesUtility/ResxLanguages" trong chuỗi list_path_folder
            // Ta được còn lại chuỗi Account và sau đó add vào mảng string list_folder
            for (int i = 0; i < list_path_folder.Length; i++)
            {
                folder = new FolderModel();
                folder.ID = i;

                folder.Name = list_path_folder[i].Replace(path, "");
                list_folder.Add(folder);
            }

            list_folder.Add(new FolderModel()
            {
                ID = list_path_folder.Length + 1,
                Name = "Tất cả các file"
            });

            //Tạo select list và gán vào ViewBag gửi cho dropdownlist bên View
            folderId = folderID;
            ViewBag.folderID_search = folderID;
            SelectList cateList = new SelectList(list_folder, "ID", "NAME");
            ViewBag.CateList = cateList;

            //Show tất cả dữ liệu
            if (folderID == list_folder.Last().ID)
            {
                ViewBag.ListItV2 = query;
                
                if (!string.IsNullOrEmpty(searchString))
                {
                    return showAllLang(searchString, folderID, ddlLanguage, ddlLanguageImport);
                }
                return View();
            }

            //Lấy Được tên folder trong list_folder tại vị trí folderID
            string folder_name = list_folder[folderID].Name;

            List<string> xmlList = strLangs.Split(',').ToList();
            List<string> xmlList_lang = strLangs.Split(',').ToList();

            var xmlList_cookie = xmlList;

            //Hiển thị theo check
            if (listLangCookie != null)
            {
                xmlList_cookie = xmlList.Intersect(listLangCookie).ToList();
                var checkEn = xmlList_cookie.Any(item => item == "En");
                //dll khong co Lang En thì thêm vào
                if (!checkEn)
                {
                    xmlList.Add("En");
                }
                xmlList = xmlList_cookie;
            }
            
            //Tạo list chứa các ngon ngữ không có trong thư mục
            listXmlNotFile = new List<string>();

            //Tạo Xelement và load files
            string name;
            List<XElement> listXelement = new List<XElement>();
            foreach (var item in xmlList)
            {
                name = "xelement" + stringToUpper(item);
                listXelement.Add(new XElement(name));
            }

            string path1 = "";
            path1 = path + folder_name + folder_name;
            for (var i = 0; i < xmlList.Count; i++)
            {
                try
                {
                    bool isconvertTempToParrent = false;
                    if (changeFolderTemp)
                    {
                        isconvertTempToParrent = convertTempToParrent(path1 + "." + xmlList[i], folder_name, xmlList[i] + ".xml");
                        if (i + 1 == xmlList.Count)
                        {
                            changeFolderTemp = false;
                        }
                    }
                    listXelement[i] = XElement.Load(path1 + "." + xmlList[i] + ".xml");

                }
                catch (Exception)
                {
                    //ko có file thì bỏ qua
                }
            }
            
            var queryEn = (from node in listXelement[0].Descendants("data")
                           select new ElementModel
                           {
                               Key = node.Attribute("name").Value,
                               En = node.Element("value").Value
                           }).ToList();


            foreach (var lang in xmlList)
            {
                try
                {
                    XElement xelementLang = XElement.Load(path1 + "." + lang + ".xml");
                    var queryLang = (from node in xelementLang.Descendants("data")
                                     select new ElementModel
                                     {
                                         Key = node.Attribute("name").Value,
                                         En = node.Element("value").Value
                                     }).ToList();

                    //kiễm tra và thêm key mới
                    //checkNewKey(queryLang, queryEn, folder_name, ListNewKey);

                    var join_lang =
                        from a in queryEn
                        join b in queryLang on a.Key equals b.Key into pb
                        from vb in pb.DefaultIfEmpty()
                        select new ElementModel
                        {
                            Key = a.Key,
                            En = vb?.En ?? string.Empty
                        };


                    if (!string.IsNullOrEmpty(searchString))
                    {
                        query.Add(new ElementV2Model()
                        {
                            Lang = lang,
                            Detail = join_lang.Where(x => x.Key.ToLower().Contains(searchString.ToLower())).Select(x => new ElementDetailModel
                            {
                                Key = x.Key,
                                Name = x.En
                            }).ToList()
                        });
                    }
                    else
                    {
                        query.Add(new ElementV2Model()
                        {
                            Lang = lang,
                            Detail = join_lang.Select(x => new ElementDetailModel
                            {
                                Key = x.Key,
                                Name = x.En
                            }).ToList()
                        });
                    }

                }
                catch (Exception)
                {
                    //ko có file

                    //query.Add(new ElementV2Model()
                    //{
                    //    Lang = lang,
                    //    Detail = new List<ElementDetailModel>()
                    //});

                    //thêm các ngôn ngữ không có file vào list
                    listXmlNotFile.Add(lang);

                }
            }

            //Xóa các ngôn ngữ không có file khỏi danh sách hiển thị
            xmlList_lang = minusList(xmlList_lang, listXmlNotFile);

            ViewBag.list_item_Lang = xmlList_lang;

            //Chuyển danh sách ngôn ngữ langsImport qua giao diện index
            List<string> xmlListImport1 = listXmlNotFile;
            SelectList selectListImport = new SelectList(xmlListImport1, "", "");
            ViewBag.listLangImport = selectListImport;


            //Tạo list header table cho View
            List<string> list_header = new List<string>();
            foreach (var item in xmlList)
            {
                list_header.Add(stringToUpper(item));
            }

            ViewBag.headerTable = list_header;

            ViewBag.ListItV2 = query;

            //lấy ngôn ngữ được chọn import
            try {
                selectLanguageImport = (ddlLanguageImport != "") ? ddlLanguageImport : listXmlNotFile[0];
            }
            catch
            {
                selectLanguageImport = "";
            }
            return View();
        }

        #region ADD
        public int DoAdd(string folder_name, List<ElementV2Model> model)
        {
            if(string.IsNullOrEmpty(folder_name) || model.Count == 0)
            {
                return -2;
            }
            var strLangs = System.Configuration.ConfigurationManager.AppSettings["Langs"];
            List<string> xmlList = new List<string>();

            List<string> lang = new List<string>();
            string key = model.FirstOrDefault().Detail.FirstOrDefault().Key;
            foreach (var item in model)
            {
                lang.Add(item.Detail.FirstOrDefault().Name);
                xmlList.Add(item.Lang);
            }

            var path = System.Configuration.ConfigurationManager.AppSettings["UrlFolder"];
            List<string> list_files = new List<string>();
            string path1 = path + folder_name + folder_name;
            for (var i = 0; i < xmlList.Count; i++)
            {
                createFileTemp(path1 + "." + xmlList[i]); // kiểm tra tồn tại và create file temp
                list_files.Add(path1 + "." + xmlList[i] + "_temp.xml");
            }
            XElement xele = null;
            try
            {
                for (int i = 0; i < list_files.Count; i++)
                {
                    xele = XElement.Load(list_files[i]);
                    var lang_value = lang[i] == null ? "" : lang[i];

                    var cust = xele.Descendants("data").FirstOrDefault(c => c.Attribute("name").Value == key);
                    if (cust == null)
                    {
                        xele.Add(new XElement("data",
                            new XAttribute("name", key),
                            new XAttribute(XNamespace.Xml + "space", "preserve"),
                            new XElement("value", lang_value)));
                        xele.Save(list_files[i]);

                    }
                    else
                    {
                        return -2;
                        // tồn tại key
                    }
                }

            }
            catch {
                //không tìm thấy file theo list
            }
            return 1;
        }
        [HttpPost]
        public JsonResult Add(string folder_name = null, List<ElementV2Model> model = null)
        {
            if (ModelState.IsValid)
            {
                var res = DoAdd(folder_name, model);
                if (res == 1)
                {
                    var strModel = JsonConvert.SerializeObject(model);
                    WriteXmlFileLink("Home/AddLanguageApi", folder_name, strModel);
                    changeFolderTemp = true;
                }
                return Json(new
                {
                    status = res
                });
            }
            else
            {
                return Json(new
                {
                    status = -1
                });
            }
        }

        [HttpPost]
        public JsonResult AddLanguageApi(string folder_name = null, string model = null)
        {
            if(string.IsNullOrEmpty(folder_name) || string.IsNullOrEmpty(model))
            {
                return Json(new
                {
                    status = -1
                });
            }
            var data = JsonConvert.DeserializeObject<List<ElementV2Model>>(model);
            var res = DoAdd(folder_name, data);


            return Json(new
            {
                status = res
            });
        }

        #endregion

        #region Write file api link 
        public static bool WriteXmlFileLink(string link ,string folder_name = null, string model = null, string key_name = null)
        {
            if (string.IsNullOrEmpty(folder_name) || string.IsNullOrEmpty(model))
            {
                return false;
            }

            var strApiLink = System.Configuration.ConfigurationManager.AppSettings["ApiLink"];
            List<string> listApiLink = strApiLink.Split(',').ToList();

            if (string.IsNullOrEmpty(strApiLink) || listApiLink.Count == 0)
                return false;

            string url = string.Empty;
            string rs = string.Empty;
            var current = string.Empty;

            foreach (var item in listApiLink)
            {
                url = string.Format("http://{0}/{1}", item.Trim(), link.Trim());
                if (!string.IsNullOrEmpty(key_name))
                {
                    current = MyUtility.Common.TrySerializeObject(new
                    {
                        folder_name = folder_name,
                        model = model,
                        key_name = key_name
                    });
                }
                else
                {
                    current = MyUtility.Common.TrySerializeObject(new
                    {
                        folder_name = folder_name,
                        model = model
                    });
                }
                
                rs = NetworkCommon.SendPOSTRequestJsonSimple(url, current);
            }

            return true;
        }
        #endregion end write api link

        #region Update

        public int DoUpdate(string folder_name, List<ElementV2Model> model, string key_name = null)
        {
            List<string> lang = new List<string>();
            string key = key_name;

            foreach (var item in model)
            {
                lang.Add(item.Detail.FirstOrDefault().Name);
            }

            var path = System.Configuration.ConfigurationManager.AppSettings["UrlFolder"];
            List<string> list_files = new List<string>();
            string path1 = path + folder_name + folder_name;
            foreach (var item in model)
            {
                createFileTemp(path1 + "." + item.Lang.ToLower()); // kiểm tra tồn tại và create file temp
                list_files.Add(string.Format("{0}{1}{2}{3}", path, folder_name, folder_name, string.Format(".{0}_temp.xml", item.Lang.ToLower())));
            }

            //list_files.Add(string.Format("{0}{1}{2}", path, folder_name, folder_name + ".en.xml"));


            XElement xele = null;
            for (int i = 0; i < list_files.Count; i++)
            {
                try
                {
                    xele = XElement.Load(list_files[i]);
                    var lang_value = lang[i] == null ? "" : lang[i];

                    var cust = xele.Descendants("data").FirstOrDefault(c => c.Attribute("name").Value == key);
                    if (cust != null)
                    {
                        cust.SetElementValue("value", lang_value);
                        xele.Save(list_files[i]);
                        
                    }
                    else
                    {
                        // Không tồn tại key
                        xele.Add(new XElement("data", new XAttribute("name", key), new XAttribute(XNamespace.Xml + "space", "preserve"), new XElement("value", lang_value)));
                        xele.Save(list_files[i]);
                    }
                }
                catch
                {
                    //không tìm thấy file theo list
                    continue;
                }

            }
            
            return 1;
        }

        [HttpPost]
        public JsonResult Update(string folder_name = null, List<ElementV2Model> model = null, string key_name = null)
        {
            var models = model;

            var res = DoUpdate(folder_name, model, key_name);
            if (res == 1)
            {
                var strModel = JsonConvert.SerializeObject(model);
                WriteXmlFileLink("Home/UpdateLanguageApi", folder_name, strModel, key_name);
                changeFolderTemp = true;
            }
            
            return Json(new
            {
                status = res
            });

        }

        [HttpPost]
        public JsonResult UpdateLanguageApi(string folder_name = null, string model = null, string key_name = null)
        {
            if (string.IsNullOrEmpty(folder_name) || string.IsNullOrEmpty(model))
            {
                return Json(new
                {
                    status = -1
                });
            }
            var data = JsonConvert.DeserializeObject<List<ElementV2Model>>(model);
            var res = DoUpdate(folder_name, data, key_name);

            return Json(new
            {
                status = res
            });
        }

        #endregion

        #region Delete
        public bool DoDelete(string folder_name, string key_name)
        {

            var path = System.Configuration.ConfigurationManager.AppSettings["UrlFolder"];
            string[] files = Directory.GetFiles(path + folder_name, "*.xml");

            XElement xele = null;

            for (int i = 0; i < files.Length; i++)
            {
                xele = XElement.Load(files[i]);

                var cust = xele.Descendants("data").FirstOrDefault(c => c.Attribute("name").Value == key_name);
                if (cust != null)
                {
                    cust.Remove();
                    xele.Save(files[i]);
                }
                else
                {
                    // Không tồn tại key
                }
            }
            return true;
        }
        [HttpPost]
        public JsonResult Delete(string folder_name = null, string key_name = null)
        {
            var res = DoDelete(folder_name, key_name);

            return Json(new
            {
                status = true
            });
        }
        #endregion

        #region Export
        [HttpPost]
        public FileResult ExportToExcel(int folderID = 0, string ddlLanguage = "En")
        {
            selectLanguage = ddlLanguage;
            //path = D:/ResxLanguagesUtility/ResxLanguages/
            var path = System.Configuration.ConfigurationManager.AppSettings["UrlFolder"];

            //Lấy list path folder theo path
            string[] list_path_folder = Directory.GetDirectories(path);

            //Tạo mới 1 List FolderModel với thuộc tính là ID , NAME
            var list_folder = new List<FolderModel>();
            FolderModel folder = null;

            //Duyệt mảng list_path_folder
            //Ở từng list_path_folder ta thay thế tất cả ký tự trùng với biến path thành ""
            //Rồi sau đó add vào mảng list_folder
            //vd 1 list_path_folder = D:/ResxLanguagesUtility/ResxLanguages/Account
            // Sau khi thay thế path = "D:/ResxLanguagesUtility/ResxLanguages" trong chuỗi list_path_folder
            // Ta được còn lại chuỗi Account và sau đó add vào mảng string list_folder
            for (int i = 0; i < list_path_folder.Length; i++)
            {
                folder = new FolderModel();
                folder.ID = i;
                folder.Name = list_path_folder[i].Replace(path, "");
                list_folder.Add(folder);
            }

            string folder_name = "";
            string path1 = "";
            var index = 0;
            XmlDocument doc = new XmlDocument();
            using (XLWorkbook wb = new XLWorkbook()) //Install ClosedXml from Nuget for XLWorkbook  
            {
                //Đọc các tất cả các file trong ResxLanguages
                foreach (var item in list_folder)
                {
                    //Tạo bản chứa nội dung trong file 
                    DataTable dt = new DataTable("Grid");
                    dt.Columns.AddRange(new DataColumn[2] {
                        new DataColumn("key"),
                        new DataColumn("Value")
                    });
                    //Lấy Được tên folder trong list_folder tại vị trí index
                    folder_name = list_folder[index].Name;

                    //dùng tên folder làm tên sheet - excel
                    var sheetName = folder_name.TrimStart('\\');
                    try {
                        path1 = path + folder_name + folder_name;

                        doc.Load(path1 + "." + selectLanguage + ".xml");
                        foreach (XmlNode node in doc.SelectNodes("/LocalLanguages/data "))
                        {
                            dt.Rows.Add(node.Attributes["name"].Value, node["value"].InnerText);
                        }
                        wb.Worksheets.Add(dt, sheetName);
                        index++;

                    }
                    catch {
                        //không có file trong thư mục
                        index++;
                        continue;
                    };

                }
                using (MemoryStream stream = new MemoryStream()) //using System.IO;  
                {
                    try
                    {
                        wb.SaveAs(stream);
                    }
                    catch { };
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", selectLanguage + ".xlsx");
                }
            }
        }
        #endregion

        #region Import
        [HttpPost]
        public JsonResult ImportFromXML(HttpPostedFileBase excelFile)
        {
            var res = doImportFromXML(excelFile);
            return Json(new
            {
                status = res
            });
        }

        public int doImportFromXML(HttpPostedFileBase excelFile)
        {
            var nameExcel = "";
            int result = 0;
            var langCurren = selectLanguageImport.ToLower();
            if (ModelState.IsValid)
            {
                if (excelFile != null && excelFile.ContentLength > 0)
                {
                    Stream stream = excelFile.InputStream;
                    IExcelDataReader reader = null;
                    if (excelFile.FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (excelFile.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        //File truyền vào không đúng định dạng
                        result = -1;
                        return result;
                    }

                    nameExcel = langCurren;

                    //path = D:/ResxLanguagesUtility/ResxLanguages/
                    var path = System.Configuration.ConfigurationManager.AppSettings["UrlFolder"];
                    var strLangs = System.Configuration.ConfigurationManager.AppSettings["Langs"];

                    List<string> xmlList = strLangs.Split(',').ToList();

                    //Lấy list path folder theo path
                    string[] list_path_folder = Directory.GetDirectories(path);

                    //Tạo mới 1 List FolderModel với thuộc tính là ID , NAME
                    var list_folder = new List<FolderModel>();
                    FolderModel folder = null;

                    //Duyệt mảng list_path_folder
                    //Ở từng list_path_folder ta thay thế tất cả ký tự trùng với biến path thành ""
                    //Rồi sau đó add vào mảng list_folder
                    //vd 1 list_path_folder = D:/ResxLanguagesUtility/ResxLanguages/Account
                    // Sau khi thay thế path = "D:/ResxLanguagesUtility/ResxLanguages" trong chuỗi list_path_folder
                    // Ta được còn lại chuỗi Account và sau đó add vào mảng string list_folder
                    for (int i = 0; i < list_path_folder.Length; i++)
                    {
                        folder = new FolderModel();
                        folder.ID = i;
                        folder.Name = list_path_folder[i].Replace(path, "");
                        list_folder.Add(folder);
                    }
                    //số lượng sheet trong file excel 
                    var legthTables = reader.AsDataSet().Tables.Count;
                    List<string> listSheet = new List<string>();
                    List<string> listSheetNotFolder = new List<string>();
                    for (var sheet = 0; sheet < legthTables; sheet++)
                    {
                        var name = reader.AsDataSet().Tables[sheet].ToString();
                        listSheetNotFolder.Add(name);
                        for (var index = 0; index < list_folder.Count; index++)
                        {
                            var nameFolder = list_folder[index].Name.TrimStart('\\');
                            if (name == list_folder[index].Name.TrimStart('\\'))
                            {
                                listSheet.Add(name);
                            }
                        }
                    }

                    //lấy danh sách cái sheet không có trong folder
                    listSheetNotFolder = minusList(listSheet, listSheetNotFolder);

                    bool check = checkFile("en", "Account");
                    try
                    {
                        for (var sheet = 0; sheet < listSheet.Count; sheet++)
                        {
                            //kiễm tra tồn tại của file trong folder
                            if (!checkFile(nameExcel, listSheet[sheet].ToString()))
                            {
                                //Decalre a new XMLDocument object
                                XmlDocument doc = new XmlDocument();

                                var pathXml = "";
                                //xml declaration is recommended, but not mandatory
                                XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);

                                //create the root element
                                XmlElement root = doc.DocumentElement;
                                doc.InsertBefore(xmlDeclaration, root);
                                XmlElement element1 = doc.CreateElement(string.Empty, "LocalLanguages", string.Empty);
                                doc.AppendChild(element1);

                                string folder_name = "\\" + listSheet[sheet].ToString();
                                //folder_name = list_folder[folderId].Name;
                                pathXml = path + folder_name + folder_name;

                                DataTable dt_ = reader.AsDataSet().Tables[listSheet[sheet].ToString()];

                                //Dòng đầu file là key, value
                                for (var i = 1; i < dt_.Rows.Count; i++)
                                {
                                    var Key = dt_.Rows[i][0].ToString();
                                    var Value = dt_.Rows[i][1].ToString();
                                    XmlElement element2 = doc.CreateElement(string.Empty, "data", string.Empty);
                                    XmlElement element3 = doc.CreateElement(string.Empty, "value", string.Empty);

                                    element1.AppendChild(element2);
                                    element2.AppendChild(element3);
                                    XmlText text1 = doc.CreateTextNode(Value);
                                    element3.AppendChild(text1);
                                    element2.SetAttribute("name", Key);

                                    doc.Save(pathXml + "." + nameExcel + ".xml");

                                }
                            }
                        }

                        for (var sheet = 0; sheet < listSheetNotFolder.Count; sheet++)
                        {
                            var nameSheet = listSheetNotFolder[sheet].ToString();
                            //Decalre a new XMLDocument object
                            XmlDocument docNotFolder = new XmlDocument();

                            var pathXml = "";
                            //xml declaration is recommended, but not mandatory
                            XmlDeclaration xmlDeclaration = docNotFolder.CreateXmlDeclaration("1.0", "UTF-8", null);

                            //create the root element
                            XmlElement root = docNotFolder.DocumentElement;
                            docNotFolder.InsertBefore(xmlDeclaration, root);
                            XmlElement element1 = docNotFolder.CreateElement(string.Empty, "LocalLanguages", string.Empty);
                            docNotFolder.AppendChild(element1);

                            string folder_name = "";
                            folder_name = list_folder[folderId].Name;
                            pathXml = path + folder_name + folder_name;

                            DataTable dt_ = reader.AsDataSet().Tables[nameSheet];

                            //Dòng đầu file là key, value
                            for (var i = 1; i < dt_.Rows.Count; i++)
                            {
                                var Key = dt_.Rows[i][0].ToString();
                                var Value = dt_.Rows[i][1].ToString();
                                XmlElement element2 = docNotFolder.CreateElement(string.Empty, "data", string.Empty);
                                XmlElement element3 = docNotFolder.CreateElement(string.Empty, "value", string.Empty);

                                element1.AppendChild(element2);
                                element2.AppendChild(element3);
                                XmlText text1 = docNotFolder.CreateTextNode(Value);
                                element3.AppendChild(text1);
                                element2.SetAttribute("name", Key);

                                docNotFolder.Save(pathXml + "." + nameSheet + "_" + nameExcel + ".xml");

                            }
                        }
                        //Import file thành công
                        result = 1;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        result = -3;
                        return result;
                    }
                    reader.Close();
                    reader.Dispose();

                }
                else
                {
                    //Chưa có file
                    result = -4;
                }
            }
            return result;
        }
        #endregion

        #region in hoa chữ cái đầu tiên trong chuỗi
        //In hoa chữ cái đầu tiên trong chuỗi
        public string stringToUpper(string str)
        {
            string str1 = str.Substring(0, 1);
            string str2 = str.Substring(1);
            str1 = str1.ToUpper();
            string result = str1 + str2;
            return result;
        }
        #endregion

        #region Kiễm tra file xml có tồn tại trong folder không
        //kiem tra file xml có tồn tại trong folder khong
        public bool checkFile(string langImport, string folderName)
        {
            bool result = true;
            //path = D:/ResxLanguagesUtility/ResxLanguages/
            var path = System.Configuration.ConfigurationManager.AppSettings["UrlFolder"];

            //Lấy Được tên folder trong list_folder tại vị trí folderID
            string folder_name = "\\" + folderName;


            //Tạo Xelement và load files
            try
            {
                string path1 = path + folder_name + folder_name;
                XElement listXelement = XElement.Load(path1 + "." + langImport + ".xml");
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region trừ 2 list
        //trừ 2 danh sách
        public List<string> minusList(List<string> a, List<string> b)
        {
            try
            {
                List<string> result = new List<string>();
                var list3 = a.Except(b);
                var list4 = b.Except(a);
                return result = list3.Concat(list4).ToList();
            }
            catch
            {
                return null;
            }

        }
        #endregion

        #region kiêm tra và thêm key mới
        //kiễm tra và thêm key mới
        public void checkNewKey(List<ElementModel> queryLang, List<ElementModel> queryEn, string folder_name, List<string> listRs)
        {
            var exists_new_key = queryLang.Where(p => !queryEn.Any(p2 => p2.Key == p.Key));
            var check_new_key = exists_new_key.ToList();

            if (check_new_key.Count != 0)
            {
                listRs = new List<string>();
                //DoAdd(folder_name,new_key)
                for (var i = 0; i < check_new_key.Count(); i++)
                {
                    var new_key_model = new List<ElementV2Model>
                            {
                                new ElementV2Model
                                {
                                    Lang = "En",
                                    Detail = new List<ElementDetailModel>()
                                    {
                                        new ElementDetailModel
                                        {
                                            Key = check_new_key[i].Key,
                                            Name = ""
                                        }
                                    }
                                }

                            };
                    DoAdd(folder_name, new_key_model);
                    listRs.Add((check_new_key[i].Key));
                }
                //view bag listNewKey
                ViewBag.ListNewKey_V = listRs;
            }

        }
        #endregion

        #region Hiển thị tất cả các file
        //Hiển thị tất cả
        public ActionResult showAllLang(string searchString = null, int folderID = 0, string ddlLanguage = "En", string ddlLanguageImport = "")
        {
            //Lấy ngôn ngữ được chọn export
            selectLanguage = ddlLanguage;

            //path = D:/ResxLanguagesUtility/ResxLanguages/
            var path = System.Configuration.ConfigurationManager.AppSettings["UrlFolder"];
            var strLangs = System.Configuration.ConfigurationManager.AppSettings["Langs"];

            //Lấy list path folder theo path
            string[] list_path_folder = Directory.GetDirectories(path);

            //Tạo mới 1 List FolderModel với thuộc tính là ID , NAME
            var list_folder = new List<FolderModel>();
            FolderModel folder = null;

            ViewBag.SearchString = searchString;

            for (int i = 0; i < list_path_folder.Length; i++)
            {
                folder = new FolderModel();
                folder.ID = i + 1;

                folder.Name = list_path_folder[i].Replace(path, "");
                list_folder.Add(folder);
            }

            list_folder.Add(new FolderModel()
            {
                ID = list_path_folder.Length + 1,
                Name = "Tất cả các file"
            });

            //Tạo select list và gán vào ViewBag gửi cho dropdownlist bên View
            folderId = folderID;
            SelectList cateList = new SelectList(list_folder, "ID", "NAME");
            ViewBag.CateList = cateList;

            List<string> xmlList = strLangs.Split(',').ToList();

            //Tạo list chứa các ngon ngữ không có trong thư mục
            listXmlNotFile = new List<string>();

            var query_list = new List<List<ElementV2Model>>();
            List<XElement> listXelement = new List<XElement>();

            string name = "";

            foreach (var item in xmlList)
            {
                name = "xelement" + stringToUpper(item);
                listXelement.Add(new XElement(name));
            }

            string path1 = "";
            var listXmlFile = new List<string>();
            foreach (var namefolder in list_folder)
            {
                if (namefolder.ID != folderID)
                {
                    path1 = path + namefolder.Name + namefolder.Name;
                    for (var i = 0; i < xmlList.Count; i++)
                    {
                        try
                        {
                            listXelement[i] = XElement.Load(path1 + "." + xmlList[i] + ".xml");
                        }
                        catch
                        {
                            //khong có file
                        }
                    }

                    var queryEn = (from node in listXelement[0].Descendants("data")
                                   select new ElementModel
                                   {
                                       Key = node.Attribute("name").Value,
                                       En = node.Element("value").Value
                                   }).ToList();

                    var query = new List<ElementV2Model>();
                    foreach (var lang in xmlList)
                    {
                        try
                        {
                            XElement xelementLang = XElement.Load(path1 + "." + lang + ".xml");

                            var queryLang = (from node in xelementLang.Descendants("data")
                                             select new ElementModel
                                             {
                                                 Key = node.Attribute("name").Value,
                                                 En = node.Element("value").Value
                                             }).ToList();

                            var join_lang =
                                from a in queryEn
                                join b in queryLang on a.Key equals b.Key into pb
                                from vb in pb.DefaultIfEmpty()
                                select new ElementModel
                                {
                                    Key = a.Key,
                                    En = vb?.En ?? string.Empty
                                };

                            if (!string.IsNullOrEmpty(searchString))
                            {
                                query.Add(new ElementV2Model()
                                {
                                    Lang = lang,
                                    Detail = join_lang.Where(x => x.Key.ToLower().Contains(searchString.ToLower())).Select(x => new ElementDetailModel
                                    {
                                        Key = x.Key,
                                        Name = x.En
                                    }).ToList()
                                });
                            }
                            else
                            {
                                query.Add(new ElementV2Model()
                                {
                                    Lang = lang,
                                    Detail = join_lang.Select(x => new ElementDetailModel
                                    {
                                        Key = x.Key,
                                        Name = x.En
                                    }).ToList()
                                });
                            }
                            listXmlFile.Add(lang);
                        }
                        catch (Exception)
                        {
                            if (!listXmlFile.Select(item => item == lang).Any())
                            {
                                listXmlNotFile.Add(lang);
                            }
                            
                        }
                    }
                    query_list.Add(query);
                }
            }

            //Xóa các ngôn ngữ không có file khỏi danh sách hiển thị
            xmlList = minusList(xmlList, listXmlNotFile);

            //Chuyển danh sách ngôn ngữ langsImport qua giao diện index
            List<string> xmlListImport1 = listXmlNotFile;
            SelectList selectListImport = new SelectList(xmlListImport1, "", "");
            ViewBag.listLangImport = selectListImport;


            List<string> list_header = new List<string>();
            foreach (var item in xmlList)
            {
                list_header.Add(stringToUpper(item));
            }
            ViewBag.ListItV3 = query_list;

            ViewBag.headerTable1 = list_header;

            return View("Index");
        }
        #endregion

        #region list Languare cookie
        [HttpGet]
        public void list_Languare_cookie(string cookie)
        {
            try
            {
                listLangCookie = cookie.Split(',').ToList();
            }
            catch
            {
            }
        }
        #endregion

        #region SearchAll
        [HttpPost]
        public JsonResult SearchAll(string searchString = null, int folderID = 0, string f_name = "", string ddlLanguage = "En", string ddlLanguageImport = "")
        {
            var query = new List<ElementV2Model>();
            ViewBag.ListSearch = query;
            //Lấy ngôn ngữ được chọn export
            selectLanguage = ddlLanguage;

            //path = D:/ResxLanguagesUtility/ResxLanguages/
            var path = System.Configuration.ConfigurationManager.AppSettings["UrlFolder"];
            var strLangs = System.Configuration.ConfigurationManager.AppSettings["Langs"];

            //Lấy list path folder theo path
            string[] list_path_folder = Directory.GetDirectories(path);

            //Tạo mới 1 List FolderModel với thuộc tính là ID , NAME
            var list_folder = new List<FolderModel>();
            FolderModel folder = null;

            ViewBag.SearchString = searchString;

            //Duyệt mảng list_path_folder
            //Ở từng list_path_folder ta thay thế tất cả ký tự trùng với biến path thành ""
            //Rồi sau đó add vào mảng list_folder
            //vd 1 list_path_folder = D:/ResxLanguagesUtility/ResxLanguages/Account
            // Sau khi thay thế path = "D:/ResxLanguagesUtility/ResxLanguages" trong chuỗi list_path_folder
            // Ta được còn lại chuỗi Account và sau đó add vào mảng string list_folder
            for (int i = 0; i < list_path_folder.Length; i++)
            {
                folder = new FolderModel();
                folder.ID = i;

                folder.Name = list_path_folder[i].Replace(path, "");
                list_folder.Add(folder);

            }

            list_folder.Add(new FolderModel()
            {
                ID = list_path_folder.Length + 1,
                Name = "Tất cả các file"
            });

            //Tạo select list và gán vào ViewBag gửi cho dropdownlist bên View
            if (folderID == 15)
            {
                folderID = list_folder.Where(item => item.Name == f_name).FirstOrDefault().ID;
            }

            folderId = folderID;
            SelectList cateList = new SelectList(list_folder, "ID", "NAME");
            ViewBag.CateList = cateList;

            //Lấy Được tên folder trong list_folder tại vị trí folderID
            string folder_name = list_folder[folderID].Name;

            List<string> xmlList = strLangs.Split(',').ToList();

            //Tạo list chứa các ngon ngữ không có trong thư mục
            listXmlNotFile = new List<string>();

            //Tạo Xelement và load files
            string name;
            List<XElement> listXelement = new List<XElement>();
            foreach (var item in xmlList)
            {
                name = "xelement" + stringToUpper(item);
                listXelement.Add(new XElement(name));
            }

            string path1 = "";

            path1 = path + folder_name + folder_name;

            for (var i = 0; i < xmlList.Count; i++)
            {
                try
                {
                    listXelement[i] = XElement.Load(path1 + "." + xmlList[i] + ".xml");
                }
                catch (Exception)
                {
                    //ko có file thì bỏ qua
                }
            }

            var queryEn = (from node in listXelement[0].Descendants("data")
                           select new ElementModel
                           {
                               Key = node.Attribute("name").Value,
                               En = node.Element("value").Value
                           }).ToList();


            foreach (var lang in xmlList)
            {
                try
                {
                    XElement xelementLang = XElement.Load(path1 + "." + lang + ".xml");
                    var queryLang = (from node in xelementLang.Descendants("data")
                                     select new ElementModel
                                     {
                                         Key = node.Attribute("name").Value,
                                         En = node.Element("value").Value
                                     }).ToList();

                    //kiễm tra và thêm key mới
                    //checkNewKey(queryLang, queryEn, folder_name, ListNewKey);

                    var join_lang =
                        from a in queryEn
                        join b in queryLang on a.Key equals b.Key into pb
                        from vb in pb.DefaultIfEmpty()
                        select new ElementModel
                        {
                            Key = a.Key,
                            En = vb?.En ?? string.Empty
                        };


                    if (!string.IsNullOrEmpty(searchString))
                    {
                        query.Add(new ElementV2Model()
                        {
                            Lang = lang,
                            Detail = join_lang.Where(x => x.Key.ToLower().Contains(searchString.ToLower())).Select(x => new ElementDetailModel
                            {
                                Key = x.Key,
                                Name = x.En
                            }).ToList()
                        });
                    }
                    else
                    {
                        query.Add(new ElementV2Model()
                        {
                            Lang = lang,
                            Detail = join_lang.Select(x => new ElementDetailModel
                            {
                                Key = x.Key,
                                Name = x.En
                            }).ToList()
                        });
                    }

                }
                catch (Exception)
                {
                    //ko có file

                    //thêm các ngôn ngữ không có file vào list
                    listXmlNotFile.Add(lang);

                }
            }

            //Xóa các ngôn ngữ không có file khỏi danh sách hiển thị
            xmlList = minusList(xmlList, listXmlNotFile);

            //Tạo list header table cho View
            List<string> list_header = new List<string>();
            foreach (var item in xmlList)
            {
                list_header.Add(stringToUpper(item));

            }
            return Json(query,  JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region file temp
        public static void createFileTemp(string path)
        {
            try
            {
                string str = path.ToLower();
                string temp = string.Format("{0}_temp.xml", str);
                string link = string.Format("{0}.xml", str);
                if (!System.IO.File.Exists(temp))
                {
                    System.IO.File.Copy(link, temp, true);
                }
            }
            catch (IOException iox)
            {
                Console.WriteLine(iox.Message);
            }
        }
        public bool convertTempToParrent(string path, string folderName, string fileName)
        {
            string str = path.ToLower();
            string temp = string.Format("{0}_temp.xml", str);
            string temp_copy = string.Format("{0}_temp_copy.xml", str);
            string foderNameBackUp = string.Format("{0}_{1}", folderName, DateTime.Now.ToString("ddMMyyyyHmmss"));
            string temp_copy_backup = string.Format("{0}{1}{1}", System.Configuration.ConfigurationManager.AppSettings["UrlFolderBackUp"], foderNameBackUp);
            string parrent= string.Format("{0}.xml", str);

            if (System.IO.File.Exists(temp) && System.IO.File.Exists(parrent))
            {
                if (!Directory.Exists(temp_copy_backup))  // if it doesn't exist, create
                    Directory.CreateDirectory(temp_copy_backup);
                try
                {
                    System.IO.File.Copy(temp, temp_copy, true);
                    
                    string strNameBackUp = string.Format("{0}{1}.{2}", temp_copy_backup, folderName, fileName.ToLower());
                    System.IO.File.Copy(parrent, strNameBackUp, true);
                    System.IO.File.Delete(parrent);
                    System.IO.File.Move(temp_copy, parrent);

                }
                catch(IOException ex)
                {
                    //Console.WriteLine(ex);
                }
            }
            return true;
        }
        #endregion end check url folder
    }
}