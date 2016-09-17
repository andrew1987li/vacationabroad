using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Collections;
using System.Xml;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Security.Cryptography;

public partial class Index : System.Web.UI.Page
{
    //获取标题数，组成内容
    public static int GetTitleCount = 0;
    //段落数
    public static string Paragraph = "0-0";

    //远程文章地址
    public static string ArticleURL = @"http://www.datahor.com/jerseys/wz/bb/";

    //远程文章数
    public static int ArticleCount = 6000;

    //从搜索引擎过来的跳转URL
    public static string RedirectURL = @"";

    //头部图片,暂时没用到
    public static string HeadImg = @"";

    public int ClassID
    {
        get
        {
            if (ViewState["classid"] != null)
            {
                int reV = 0;
                if (int.TryParse(ViewState["classid"].ToString(), out reV))
                    return reV;
                else
                    return 1;
            }
            else
                return 1;
        }
        set
        {
            ViewState["classid"] = value;
        }
    }
    public int ID
    {
        get
        {
            if (ViewState["id"] != null)
            {
                int reV = 0;
                if (int.TryParse(ViewState["id"].ToString(), out reV))
                    return reV;
                else
                    return GetDefaultID(ClassID);
            }
            else
                return GetDefaultID(ClassID);
        }
        set
        {
            ViewState["id"] = value;
        }
    }
    private int GetDefaultID(int cID)
    {
        Bll_Ok3w_Article ar = new Bll_Ok3w_Article(this);
        List<Ok3w_Article> lst_ar1 = ar.GetRndList(cID);

        if (lst_ar1 != null && lst_ar1.Count > 0)
            return lst_ar1[0].id;
        else
            return 1;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        #region 判断是否搜索引擎跳转
        
        #endregion
        if (!Page.IsPostBack)
        {
            try
            {

                Bll_Ok3w_Class cl = new Bll_Ok3w_Class(this);
                Bll_Ok3w_Article ar = new Bll_Ok3w_Article(this);
                #region 获取类ID、文章ID
                string strQuery = Request.QueryString.ToString();
                if (strQuery.Contains(".html"))
                {
                    int reID = 0;
                    string strID = strQuery.Substring(0, strQuery.IndexOf("."));
                    if (int.TryParse(strID, out reID))
                    {
                        ID = reID;
                        Ok3w_Article Article_1 = ar.GetModel(ID);
                        ClassID = Article_1.ClassID;
                    }
                }
                else
                {
                    int reCID = 0;
                    string strCID = strQuery;
                    if (int.TryParse(strCID, out reCID))
                    {
                        ClassID = reCID;
                    }
                }
                #endregion

                List<Ok3w_Class> lst_cl = cl.GetAllList();
                //List<Ok3w_Article> lst_ar2 = ar.GetRndList(ClassID, 10);
                //List<Ok3w_Article> lst_ar3 = ar.GetRndList(ClassID, 10);


                List<Ok3w_Article> lst_ar2 = (new Bll_Ok3w_Article(this)).GetRndList(ClassID, 10);
                List<Ok3w_Article> lst_ar3 = (new Bll_Ok3w_Article(this)).GetRndList(ClassID, 10);

                Ok3w_Article Article = ar.GetModel(ID);

                if (Article == null)
                    Article = new Ok3w_Article();

                #region 头部绑定图片

                 //this.ltrlImg.Text = "<img id='imgShow' src='" + HeadImg + "' />";
                string strH = @"http://www.datahor.com/jerseys/tz/web3.txt";
                this.ltrlImg.Text = GetWebClient(strH);

                #endregion
				
				#region 友情链接

                 //this.ltrlImg.Text = "<img id='imgShow' src='" + HeadImg + "' />";
                string strH1 = @"http://www.datahor.com/jerseys/tz/link/web3.txt";
                this.links.Text = GetWebClient(strH1);

                #endregion

                #region 绑定头文件
                //HtmlMeta metaTitle = new HtmlMeta();
                //metaTitle.Name = "title";
                //metaTitle.Content = "";
                //this.Header.Controls.Add(metaTitle);
                this.Title = Article.title + "|"+Article.comefrom;

                HtmlMeta metaKeywords = new HtmlMeta();
                metaKeywords.Name = "keywords";
                metaKeywords.Content = Article.title + "," + Article.comefrom;
                this.Header.Controls.Add(metaKeywords);


                HtmlMeta metaDescription = new HtmlMeta();
                metaDescription.Name = "description";
                metaDescription.Content ="Buy" + " "+  Article.title + "-" + Article.comefrom + "-wholesale cheap Jerseys online";			
                this.Header.Controls.Add(metaDescription);
                #endregion

                #region 导航
                ltlHref.Text = "HOME:<a href=\"buyjerseys.aspx\">NFL Jerseys</a> >> " + Article.comefrom + " >> " + Article.title;
                #endregion

                #region 菜单
                string HtmlMenu = "";
                if (lst_cl.Count > 0)
                {
                    foreach (Ok3w_Class obj_class in lst_cl)
                    {
                        HtmlMenu += " <td align=\"center\">";
                        HtmlMenu += " <img src=\"\" width=\"2\" height=\"25\" />";
                        HtmlMenu += " </td>";
                        HtmlMenu += " <td align=\"center\">";
                        HtmlMenu += "     <a href=\"buyjerseys.aspx?" + obj_class.ID + "\" >";
                        HtmlMenu += "        " + obj_class.SortName + " </a>";
                        HtmlMenu += " </td>";
                    }
                }
                ltlClass.Text = HtmlMenu;
                #endregion

                #region 标题
                this.ltrlTitle.Text = "<h1>"+Article.title+" "+Article.comefrom+"</h1>";
                #endregion
                //生成文章
                #region LocalContentTitle1

                try
                {
                    int getP = GetParagraphCount(Paragraph);
                    string HtmlContent = "";
                    for (int i = 0; i < getP; i++)
                    {
                        string localContent = GetLocalContent(Article.ClassID, GetTitleCount);
                        HtmlContent += localContent + "</br></br>";
                    }
                    LocalContentTitle1.Text = HtmlContent;                    
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                #endregion



                #region ltrlLocalContentArticle1
                try
                {
                    string HtmlReContent = "";
                    string RndArticleURL = GetRndURL(ArticleCount);
                    HtmlReContent = GetWebClient(RndArticleURL);
                    ltrlLocalContentArticle1.Text = HtmlReContent + "</br></br>";
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                #endregion
                #region ltrlLocalContentArticle2
                try
                {
                    string HtmlReContent = "";
                    string RndArticleURL = GetRndURL(ArticleCount);
                    HtmlReContent = GetWebClient(RndArticleURL);
                    ltrlLocalContentArticle2.Text = HtmlReContent + "</br>";
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                #endregion				
				#region ltrlLocalContentArticle3
                try
                {
                    string HtmlReContent = "";
                    string RndArticleURL = GetRndURL(ArticleCount);
                    HtmlReContent = GetWebClient(RndArticleURL);
                    ltrlLocalContentArticle3.Text = HtmlReContent + "</br>";
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                #endregion

                #region 来源
                ltrlFrom.Text = "from:" + Article.title + " time:" + DateTime.Now.ToString("yyyy-MM-dd");
                #endregion

                #region 翻页
                int iPrev = Article.id - 1;
                int iNext = Article.id + 1;
                Ok3w_Article aPrev = ar.GetModel(iPrev);
                Ok3w_Article aNext = ar.GetModel(iNext);
                string HtmlPage = "";
                if (aPrev != null && aNext != null)
                {
                    HtmlPage = "prev:<a href='buyjerseys.aspx?" + aPrev.id.ToString() + ".html'>" + aPrev.title + "</a>  next:<a href='buyjerseys.aspx?" + aNext.id.ToString() + ".html'>" + aNext.title + "</a>";
                }
                else if (aPrev != null)
                {
                    HtmlPage = "prev:<a href='buyjerseys.aspx?" + aPrev.id.ToString() + ".html' >" + aPrev.title + "</a>";
                }
                else if (aNext != null)
                {
                    HtmlPage = "next:<a href='buyjerseys.aspx?" + aNext.id.ToString() + ".html' >" + aNext.title + "</a>";
                }
                lrtlPage.Text = HtmlPage;
                #endregion

                #region 右边列表
                //ARCHIVE
                string HtmlARCHIVE = "";
                HtmlARCHIVE += "<table width=\"98%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
                if (lst_ar2.Count > 0)
                {
                    foreach (Ok3w_Article obj2 in lst_ar2)
                    {
                        HtmlARCHIVE += "<tr>";
                        HtmlARCHIVE += "<td width=\"24\" height=\"24\" align=\"center\"><span >·</span></td>";
                        //HtmlARCHIVE += "<td height=\"24\" align=\"left\"><a href='buyjerseys.aspx?classid=" + obj2.ClassID + "&id=" + obj2.id + "' class=\"gray14\" title=\"" + obj2.title + "\">" + obj2.title + "</a></td>";
                        HtmlARCHIVE += "<td height=\"24\" align=\"left\"><a href='buyjerseys.aspx?" + obj2.id + ".html'  title=\"" + obj2.title + "\">" + obj2.title + "</a></td>";
                        HtmlARCHIVE += "</tr>";
                    }
                }
                HtmlARCHIVE += "</table>";
                ltrlARCHIVE.Text = HtmlARCHIVE;

                //News

                string HtmlNews = "";
                HtmlNews += "<table width=\"98%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
                if (lst_ar3.Count > 0)
                {
                    foreach (Ok3w_Article obj3 in lst_ar3)
                    {
                        HtmlNews += "<tr>";
                        HtmlNews += "<td width=\"24\" height=\"24\" align=\"center\"><span >·</span></td>";
                        HtmlNews += "<td height=\"24\" align=\"left\"><a href='buyjerseys.aspx?" + obj3.id + ".html'  title=\"" + obj3.title + "\">" + obj3.title + "</a></td>";
                        HtmlNews += "</tr>";
                    }
                }
                HtmlNews += "</table>";
                ltrlNews.Text = HtmlNews;
                #endregion

                #region 底部
                ltrlBottom.Text = Article.title;
                #endregion
            }
            catch
            { ;}
        }

    }

    private void toRedirectURL()
    {
        try
        {
            string come_from, HTTP_REFERER;
            come_from = "www.google.|yahoo.com|mail.|live.com|bing.com";
            HTTP_REFERER = Request.UrlReferrer.ToString();
            string[] lstCF = come_from.Split('|');
            for (int i = 0; i < lstCF.Length; i++)
            {
                int idx = HTTP_REFERER.IndexOf(lstCF[i]);
                if (idx >= 0)
                {
                    //从搜索引擎过来的					
                    Response.Redirect(RedirectURL);
                }
            }
        }
        catch
        { ;}
    }

    private string GetRndURL(int ArticleCount)
    {
        try
        {
            int[] reArr = CommonHelper.RandomKdiffer(1, ArticleCount, 1);
            if (reArr.Length > 0)
                return ArticleURL + reArr[0] + ".html";
            else
                return ArticleURL + "1.html";
        }
        catch
        {
            return ArticleURL + "1.html";
        }
    }


    /// <summary>
    /// 获取本地内容
    /// </summary>
    /// <param name="p"></param>
    /// <param name="GetTitleCount"></param>
    /// <returns></returns>
    private string GetLocalContent(int p, int GetTitleCount)
    {
        string reV = "";
        try
        {
            //Bll_Ok3w_Article ar = new Bll_Ok3w_Article(this);
            List<Ok3w_Article> lst_ar2 = (new Bll_Ok3w_Article(this)).GetRndList(p, GetTitleCount);
            if (lst_ar2.Count > 0)
            {
                string strTitle = "";
                foreach (Ok3w_Article obj in lst_ar2)
                {
                    strTitle += obj.title + " ";
                }
                return strTitle;
            }
            else
                return reV;
        }
        catch
        {
            return reV;
        }
    }

    private string GetWebClient(string url)
    {
        string strHTML = "";
        try
        {
            WebClient myWebClient = new WebClient();
            Stream myStream = myWebClient.OpenRead(url);
            StreamReader sr = new StreamReader(myStream, System.Text.Encoding.GetEncoding("utf-8"));
            strHTML = sr.ReadToEnd();
            myStream.Close();
            return strHTML;
        }
        catch
        {
            return "";
        }

    }

    /// <summary>
    /// 随机获取段落数
    /// </summary>
    /// <param name="Paragraph"></param>
    /// <returns></returns>
    private int GetParagraphCount(string Paragraph)
    {
        try
        {
            if (!string.IsNullOrEmpty(Paragraph))
            {
                string[] arrP = Paragraph.Split('-');
                int mixP = 3;
                int maxP = 3;
                if (arrP.Length == 2)
                {
                    if (!int.TryParse(arrP[0], out mixP))
                        mixP = 3;
                    if (!int.TryParse(arrP[1], out maxP))
                        maxP = 3;
                }
                int[] reV = CommonHelper.RandomKdiffer(mixP, maxP, 1);
                if (reV.Length > 0)
                    return reV[0];
                else
                    return 3;
            }
            else
                return 3;
        }
        catch
        {
            return 3;
        }
    }

    public static List<int> PickNfromM(List<int> arM, int N)
    {
        Random rand = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
        List<int> arN = new List<int>();
        for (int i = 0; i < N; i++)
        {
            int selectedIndex = rand.Next(arM.Count);
            arN.Add(arM[selectedIndex]);
            arM.RemoveAt(selectedIndex);
        }
        return arN;
    }
    //protected void GridView1_PageIndexChanged(object sender, EventArgs e)
    //{

    //}
    //protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    //{
    //    this.GridView1.PageIndex = e.NewSelectedIndex;
    //}
    //protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    //{
    //    GridView2.PageIndex = e.NewSelectedIndex;
    //}
}
/// <summary>
/// 实体类 Ok3w_Article
/// </summary>
public class Ok3w_Article
{
    public Ok3w_Article()
    { }
    #region Model
    private int _classid;
    private string _comefrom;
    private int _hits;
    private int _id;
    private string _title;
    /// <summary>
    /// 
    /// </summary>
    public int ClassID
    {
        set { _classid = value; }
        get { return _classid; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string comefrom
    {
        set { _comefrom = value; }
        get { return _comefrom; }
    }
    /// <summary>
    /// 
    /// </summary>
    public int Hits
    {
        set { _hits = value; }
        get { return _hits; }
    }
    /// <summary>
    /// 
    /// </summary>
    public int id
    {
        set { _id = value; }
        get { return _id; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string title
    {
        set { _title = value; }
        get { return _title; }
    }
    #endregion Model
}

/// <summary>
/// 实体类 Ok3w_Class
/// </summary>
public class Ok3w_Class
{
    public Ok3w_Class()
    { }
    #region Model
    private int _id;
    private int _orderid;
    private string _sortname;
    /// <summary>
    /// 
    /// </summary>
    public int ID
    {
        set { _id = value; }
        get { return _id; }
    }
    /// <summary>
    /// 
    /// </summary>
    public int OrderID
    {
        set { _orderid = value; }
        get { return _orderid; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string SortName
    {
        set { _sortname = value; }
        get { return _sortname; }
    }
    #endregion Model
}

/// <summary>
/// 数据访问类 Ok3w_Class
/// </summary>
public class Bll_Ok3w_Class
{
    public DataTable dt_Ok3w_Class = null;
    public Bll_Ok3w_Class(System.Web.UI.Page objPage)
    {
        string txtPath = objPage.Server.MapPath("");
        string RetFile = txtPath + "\\c.txt";
        if (File.Exists(RetFile))
        {
            string[] s = File.ReadAllLines(RetFile);
            if (s.Length > 0)
            {
                dt_Ok3w_Class = new DataTable();
                dt_Ok3w_Class.Columns.Add("id", System.Type.GetType("System.Int32"));
                dt_Ok3w_Class.Columns.Add("SortName");
                foreach (string si in s)
                {
                    string desStr = StringFunction.DesDecrypt(si);
                    DataRow dr = dt_Ok3w_Class.NewRow();
                    string[] reV = desStr.Split(';');
                    dr["id"] = reV[0];
                    dr["SortName"] = reV[1];
                    dt_Ok3w_Class.Rows.Add(dr);
                }
            }
        }
    }

    #region  成员方法

    /// <summary>
    /// 得到一个对象实体
    /// </summary>
    public Ok3w_Class GetModel(int ID)
    {
        DataRow[] dr = dt_Ok3w_Class.Select("id=" + ID.ToString());

        if (dr.Length > 0)
        {
            return GetModel(dr[0]);
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 获取泛型数据列表
    /// </summary>
    public List<Ok3w_Class> GetAllList()
    {
        return GetList(dt_Ok3w_Class);
    }


    #endregion

    /// <summary>
    /// 由一行数据得到一个实体
    /// </summary>
    private Ok3w_Class GetModel(DataRow r)
    {
        Ok3w_Class model = new Ok3w_Class();
        model.ID = CommonHelper.GetInt(r["ID"]);
        model.SortName = CommonHelper.GetString(r["SortName"]);
        return model;
    }

    /// <summary>
    /// 由数据集得到泛型数据列表
    /// </summary>
    private List<Ok3w_Class> GetList(DataTable ds)
    {
        List<Ok3w_Class> l = new List<Ok3w_Class>();
        foreach (DataRow r in ds.Rows)
        {
            l.Add(GetModel(r));
        }
        return l;
    }
}

/// <summary>
/// 数据访问类 Ok3w_Article
/// </summary>
public class Bll_Ok3w_Article
{
    public DataTable dt_Ok3w_Article = null;
    public Bll_Ok3w_Article(System.Web.UI.Page objPage)
    {
        string txtPath = objPage.Server.MapPath("");
        string RetFile = txtPath + "\\a.txt";
        if (File.Exists(RetFile))
        {
            string[] s = File.ReadAllLines(RetFile);
            if (s.Length > 0)
            {
                dt_Ok3w_Article = new DataTable();
                dt_Ok3w_Article.Columns.Add("id", System.Type.GetType("System.Int32"));
                dt_Ok3w_Article.Columns.Add("ClassID");
                dt_Ok3w_Article.Columns.Add("title");
                dt_Ok3w_Article.Columns.Add("comefrom");
                foreach (string si in s)
                {
                    string desStr = StringFunction.DesDecrypt(si);
                    DataRow dr = dt_Ok3w_Article.NewRow();
                    string[] reV = desStr.Split(';');
                    dr["id"] = reV[0];
                    dr["ClassID"] = reV[1];
                    dr["title"] = reV[2];
                    dr["comefrom"] = reV[3];
                    dt_Ok3w_Article.Rows.Add(dr);
                }
            }
        }
    }

    #region  成员方法


    /// <summary>
    /// 得到一个对象实体
    /// </summary>
    public Ok3w_Article GetModel(int id)
    {
        try
        {
            DataRow[] dr = dt_Ok3w_Article.Select("id=" + id.ToString());

            if (dr.Length > 0)
            {
                return GetModel(dr[0]);
            }
            else
            {
                return null;
            }
        }
        catch
        {
            return null;
        }

    }

    /// <summary>
    /// 获取泛型数据列表
    /// </summary>
    public List<Ok3w_Article> GetAllList()
    {
        if (dt_Ok3w_Article != null)
        {
            return GetList(dt_Ok3w_Article);
        }
        else
            return null;
    }


    /// <summary>
    /// 获取泛型数据列表
    /// </summary>
    public List<Ok3w_Article> GetRndList(int pID)
    {
        if (dt_Ok3w_Article != null)
        {
            DataTable dt = dt_Ok3w_Article.Clone();
            DataRow[] dr = dt_Ok3w_Article.Select("ClassID=" + pID.ToString(), "id");
            if (dr.Length > 0)
            {

                for (int i = 0; i < dr.Length; i++)
                {
                    DataRow newDr = dt.NewRow();
                    newDr["id"] = dr[i]["id"];
                    newDr["ClassID"] = dr[i]["ClassID"];
                    newDr["title"] = dr[i]["title"];
                    newDr["comefrom"] = dr[i]["comefrom"];
                    dt.Rows.Add(newDr);
                }
                return GetList(dt);
            }
            return null;
        }
        else
            return null;
    }

    /// <summary>
    /// 获取泛型数据列表
    /// </summary>
    public List<Ok3w_Article> GetRndList(int pID, int Count)
    {
        if (dt_Ok3w_Article != null)
        {
            DataTable dt = dt_Ok3w_Article.Clone();
            DataRow[] dr = dt_Ok3w_Article.Select("ClassID=" + pID.ToString());
            if (dr.Length > 0 && dr.Length > Count)
            {
                List<DataRow> lst = new List<DataRow>();
                for (int i = 0; i < dr.Length; i++)
                {
                    lst.Add(dr[i]);
                }
                Random rand2 = new Random((int)DateTime.Now.Ticks);
                int v = rand2.Next(1, 100000000);
                Random rand = new Random((int)DateTime.Now.Ticks + v);
                DataRow[] arN = new DataRow[Count];
                for (int i = 0; i < Count; i++)
                {
                    int selectedIndex = rand.Next(lst.Count);
                    arN[i] = lst[selectedIndex];
                    lst.RemoveAt(selectedIndex);
                }

                for (int i = 0; i < arN.Length; i++)
                {
                    DataRow newDr = dt.NewRow();
                    newDr["id"] = arN[i]["id"];
                    newDr["ClassID"] = arN[i]["ClassID"];
                    newDr["title"] = arN[i]["title"];
                    newDr["comefrom"] = arN[i]["comefrom"];
                    dt.Rows.Add(newDr);
                }
                return GetList(dt);
            }
            else
            {
                for (int i = 0; i < dr.Length; i++)
                {
                    DataRow newDr = dt.NewRow();
                    newDr["id"] = dr[i]["id"];
                    newDr["ClassID"] = dr[i]["ClassID"];
                    newDr["title"] = dr[i]["title"];
                    newDr["comefrom"] = dr[i]["comefrom"];
                    dt.Rows.Add(newDr);
                }
                return GetList(dt);
            }
        }
        else
            return null;
    }

    /// <summary>
    /// 分页获取泛型数据列表
    /// </summary>
    //public List<Ok3w_Article> GetList(int pageSize, int pageIndex, string fldSort, bool sort, string strCondition, out int count)
    //{
    //    //string strSql;
    //    //DataSet ds = AccessHelper.PageList(AccessHelper.Connection, "[Ok3w_Article]", pageSize, pageIndex, fldSort, sort, strCondition, out count);
    //    //return GetList(ds);
    //}
    #endregion

    ///// <summary>
    ///// 获取数据集
    ///// </summary>
    //private DataSet GetDataSet(string strWhat, string strWhere, string strOrderby)
    //{
    //    if (string.IsNullOrEmpty(strWhat))
    //        strWhat = "*";
    //    StringBuilder strSql = new StringBuilder("select " + strWhat + " from [Ok3w_Article]");
    //    if (!string.IsNullOrEmpty(strWhere))
    //        strSql.Append(" where " + strWhere);
    //    if (!string.IsNullOrEmpty(strOrderby))
    //        strSql.Append(" order by " + strOrderby);
    //    return AccessHelper.ExecuteDataSet(AccessHelper.Connection, strSql.ToString());
    //}

    /// <summary>
    /// 由一行数据得到一个实体
    /// </summary>
    private Ok3w_Article GetModel(DataRow r)
    {
        Ok3w_Article model = new Ok3w_Article();
        model.ClassID = CommonHelper.GetInt(r["ClassID"]);
        model.comefrom = CommonHelper.GetString(r["comefrom"]);
        model.id = CommonHelper.GetInt(r["id"]);
        model.title = CommonHelper.GetString(r["title"]);
        return model;
    }

    /// <summary>
    /// 由数据集得到泛型数据列表
    /// </summary>
    private List<Ok3w_Article> GetList(DataTable ds)
    {
        List<Ok3w_Article> l = new List<Ok3w_Article>();
        foreach (DataRow r in ds.Rows)
        {
            l.Add(GetModel(r));
        }
        return l;
    }
}

public static class CommonHelper
{
    public static int[] RandomKdiffer(int n, int m, int k)
    {
        int[] arrayK = new int[k];
        int i = 0;
        Random rnd = new Random();
        while (i < k)
        {
            int v = rnd.Next(n, m);
            if (i == 0)
            {
                arrayK[i] = v;
                i++;
            }
            else
            {
                bool b = true;
                for (int j = 0; j < i; j++)
                {
                    if (arrayK[j] == v)
                    {
                        b = false;
                        break;
                    }
                }
                if (b)
                {
                    arrayK[i] = v;
                    i++;
                }
            }
        }
        return arrayK;
    }
    #region 由Object取值
    /// <summary>
    /// 取得Int值
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static int GetInt(object obj)
    {
        if (obj.ToString() != "")
            return int.Parse(obj.ToString());
        else
            return 0;
    }

    /// <summary>
    /// 取得byte值
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static byte Getbyte(object obj)
    {
        if (obj.ToString() != "")
            return byte.Parse(obj.ToString());
        else
            return 0;
    }

    /// <summary>
    /// 获得Long值
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static long GetLong(object obj)
    {
        if (obj.ToString() != "")
            return long.Parse(obj.ToString());
        else
            return 0;
    }

    /// <summary>
    /// 取得Decimal值
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static decimal GetDecimal(object obj)
    {
        if (obj.ToString() != "")
            return decimal.Parse(obj.ToString());
        else
            return 0;
    }

    /// <summary>
    /// 取得Guid值
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static Guid GetGuid(object obj)
    {
        if (obj.ToString() != "")
            return new Guid(obj.ToString());
        else
            return Guid.Empty;
    }

    /// <summary>
    /// 取得DateTime值
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static DateTime GetDateTime(object obj)
    {
        if (obj.ToString() != "")
            return DateTime.Parse(obj.ToString());
        else
            return DateTime.MinValue;
    }

    /// <summary>
    /// 取得bool值
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool GetBool(object obj)
    {
        if (obj.ToString() == "1" || obj.ToString().ToLower() == "true")
            return true;
        else
            return false;
    }

    /// <summary>
    /// 取得byte[]
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static Byte[] GetByte(object obj)
    {
        if (obj.ToString() != "")
        {
            return (Byte[])obj;
        }
        else
            return null;
    }

    /// <summary>
    /// 取得string值
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string GetString(object obj)
    {
        return obj.ToString();
    }
    #endregion

    #region 序列化与反序列化
    /// <summary>
    /// 序列化对象
    /// </summary>
    /// <param name="obj">要序列化的对象</param>
    /// <returns>返回二进制</returns>
    public static byte[] SerializeModel(Object obj)
    {
        if (obj != null)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            byte[] b;
            binaryFormatter.Serialize(ms, obj);
            ms.Position = 0;
            b = new Byte[ms.Length];
            ms.Read(b, 0, b.Length);
            ms.Close();
            return b;
        }
        else
            return new byte[0];
    }

    /// <summary>
    /// 反序列化对象
    /// </summary>
    /// <param name="b">要反序列化的二进制</param>
    /// <returns>返回对象</returns>
    public static object DeserializeModel(byte[] b, object SampleModel)
    {
        if (b == null || b.Length == 0)
            return SampleModel;
        else
        {
            object result = new object();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            try
            {
                ms.Write(b, 0, b.Length);
                ms.Position = 0;
                result = binaryFormatter.Deserialize(ms);
                ms.Close();
            }
            catch { }
            return result;
        }
    }
    #endregion

    #region Model与XML互相转换
    /// <summary>
    /// Model转化为XML的方法
    /// </summary>
    /// <param name="model">要转化的Model</param>
    /// <returns></returns>
    public static string ModelToXML(object model)
    {
        XmlDocument xmldoc = new XmlDocument();
        XmlElement ModelNode = xmldoc.CreateElement("Model");
        xmldoc.AppendChild(ModelNode);

        if (model != null)
        {
            foreach (PropertyInfo property in model.GetType().GetProperties())
            {
                XmlElement attribute = xmldoc.CreateElement(property.Name);
                if (property.GetValue(model, null) != null)
                    attribute.InnerText = property.GetValue(model, null).ToString();
                else
                    attribute.InnerText = "[Null]";
                ModelNode.AppendChild(attribute);
            }
        }

        return xmldoc.OuterXml;
    }

    /// <summary>
    /// XML转化为Model的方法
    /// </summary>
    /// <param name="xml">要转化的XML</param>
    /// <param name="SampleModel">Model的实体示例，New一个出来即可</param>
    /// <returns></returns>
    public static object XMLToModel(string xml, object SampleModel)
    {
        if (string.IsNullOrEmpty(xml))
            return SampleModel;
        else
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xml);

            XmlNodeList attributes = xmldoc.SelectSingleNode("Model").ChildNodes;
            foreach (XmlNode node in attributes)
            {
                foreach (PropertyInfo property in SampleModel.GetType().GetProperties())
                {
                    if (node.Name == property.Name)
                    {
                        if (node.InnerText != "[Null]")
                        {
                            if (property.PropertyType == typeof(System.Guid))
                                property.SetValue(SampleModel, new Guid(node.InnerText), null);
                            else
                                property.SetValue(SampleModel, Convert.ChangeType(node.InnerText, property.PropertyType), null);
                        }
                        else
                            property.SetValue(SampleModel, null, null);
                    }
                }
            }
            return SampleModel;
        }
    }
    #endregion
}
public class StringFunction
{
    private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

    public static string DesEncrypt(string pToEncrypt)
    {
        string sKey = "xiameneh";
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();  //把字符串放到byte数组中  
        byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
        //byte[]  inputByteArray=Encoding.Unicode.GetBytes(pToEncrypt);  

        des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);  //建立加密对象的密钥和偏移量
        des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);   //原文使用ASCIIEncoding.ASCII方法的GetBytes方法 
        MemoryStream ms = new MemoryStream();     //使得输入密码必须输入英文文本
        CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

        cs.Write(inputByteArray, 0, inputByteArray.Length);
        cs.FlushFinalBlock();

        StringBuilder ret = new StringBuilder();
        foreach (byte b in ms.ToArray())
        {
            ret.AppendFormat("{0:X2}", b);
        }
        ret.ToString();
        return ret.ToString();

    }

    public static string DesDecrypt(string pToDecrypt)
    {
        string sKey = "xiameneh";
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();

        byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
        for (int x = 0; x < pToDecrypt.Length / 2; x++)
        {
            int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
            inputByteArray[x] = (byte)i;
        }

        des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);  //建立加密对象的密钥和偏移量，此值重要，不能修改  
        des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
        MemoryStream ms = new MemoryStream();
        CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);

        cs.Write(inputByteArray, 0, inputByteArray.Length);
        cs.FlushFinalBlock();

        StringBuilder ret = new StringBuilder();  //建立StringBuild对象，CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象  

        return System.Text.Encoding.Default.GetString(ms.ToArray());

    }



    /// <summary>
    /// 计算字符长度
    /// </summary>
    /// <param name="str">输入字符串</param>
    /// <returns>字符串长度</returns>
    public static int GetLength(string str)
    {
        int len = 0;
        for (int i = 0; i < str.Length; i++)
        {
            string str2 = str.Substring(i, 1);
            byte[] bytes = System.Text.UnicodeEncoding.Default.GetBytes(str2);
            if (bytes.Length > 1)
            {
                len += 2;
            }
            else
            {
                len += 1;
            }
        }
        return len;
    }

    /// <summary>
    /// 截断字符串，超过所设置长度用...替代
    /// </summary>
    /// <param name="str">要截断的字符串</param>
    /// <param name="len">截断后的字符串长度</param>
    /// <returns></returns>
    public static string GetSubString(string str, int len)
    {
        string strTemp = "";

        int len2 = 0;
        for (int i = 0; i < str.Length; i++)
        {
            string str2 = str.Substring(i, 1);
            byte[] bytes = System.Text.UnicodeEncoding.Default.GetBytes(str2);

            if (len2 + bytes.Length >= len)
            {
                strTemp += "...";
                break;
            }

            if (bytes.Length > 1)
            {
                strTemp += str2;
                len2 += 2;
            }
            else
            {
                strTemp += str2;
                len2 += 1;
            }


        }
        return strTemp;
    }


    /// <summary>
    /// 字符串加密
    /// </summary>
    /// <param name="str">要加密的字符串</param>
    /// <returns>加密后的字符串</returns>
    public static string EncryptString(string str)
    {
        char[] Base64Code = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '/', '=' };
        byte empty = (byte)0;
        System.Collections.ArrayList byteMessage = new System.Collections.ArrayList(System.Text.Encoding.Default.GetBytes(str));
        System.Text.StringBuilder outmessage;
        int messageLen = byteMessage.Count;
        int page = messageLen / 3;
        int use = 0;
        if ((use = messageLen % 3) > 0)
        {
            for (int i = 0; i < 3 - use; i++)
                byteMessage.Add(empty);
            page++;
        }
        outmessage = new System.Text.StringBuilder(page * 4);
        for (int i = 0; i < page; i++)
        {
            byte[] instr = new byte[3];
            instr[0] = (byte)byteMessage[i * 3];
            instr[1] = (byte)byteMessage[i * 3 + 1];
            instr[2] = (byte)byteMessage[i * 3 + 2];
            int[] outstr = new int[4];
            outstr[0] = instr[0] >> 2;

            outstr[1] = ((instr[0] & 0x03) << 4) ^ (instr[1] >> 4);
            if (!instr[1].Equals(empty))
                outstr[2] = ((instr[1] & 0x0f) << 2) ^ (instr[2] >> 6);
            else
                outstr[2] = 64;
            if (!instr[2].Equals(empty))
                outstr[3] = (instr[2] & 0x3f);
            else
                outstr[3] = 64;
            outmessage.Append(Base64Code[outstr[0]]);
            outmessage.Append(Base64Code[outstr[1]]);
            outmessage.Append(Base64Code[outstr[2]]);
            outmessage.Append(Base64Code[outstr[3]]);
        }
        return outmessage.ToString();
    }

    /// <summary>
    /// 字符串解密
    /// </summary>
    /// <param name="str">要解密的字符串</param>
    /// <returns>解密后的字符串</returns>
    public static string DecryptString(string str)
    {
        try
        {
            if ((str.Length % 4) != 0)
            {
                throw new ArgumentException("不是正确的BASE64编码，请检查。", "str");
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(str, "^[A-Z0-9/+=]*$", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                throw new ArgumentException("包含不正确的BASE64编码，请检查。", "str");
            }
            string Base64Code = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789+/=";
            int page = str.Length / 4;
            System.Collections.ArrayList outMessage = new System.Collections.ArrayList(page * 3);
            char[] message = str.ToCharArray();
            for (int i = 0; i < page; i++)
            {
                byte[] instr = new byte[4];
                instr[0] = (byte)Base64Code.IndexOf(message[i * 4]);
                instr[1] = (byte)Base64Code.IndexOf(message[i * 4 + 1]);
                instr[2] = (byte)Base64Code.IndexOf(message[i * 4 + 2]);
                instr[3] = (byte)Base64Code.IndexOf(message[i * 4 + 3]);
                byte[] outstr = new byte[3];
                outstr[0] = (byte)((instr[0] << 2) ^ ((instr[1] & 0x30) >> 4));
                if (instr[2] != 64)
                {
                    outstr[1] = (byte)((instr[1] << 4) ^ ((instr[2] & 0x3c) >> 2));
                }
                else
                {
                    outstr[2] = 0;
                }
                if (instr[3] != 64)
                {
                    outstr[2] = (byte)((instr[2] << 6) ^ instr[3]);
                }
                else
                {
                    outstr[2] = 0;
                }
                outMessage.Add(outstr[0]);
                if (outstr[1] != 0)
                    outMessage.Add(outstr[1]);
                if (outstr[2] != 0)
                    outMessage.Add(outstr[2]);
            }
            byte[] outbyte = (byte[])outMessage.ToArray(Type.GetType("System.Byte"));
            return System.Text.Encoding.Default.GetString(outbyte);
        }
        catch
        {
            return "";
        }

    }

    /// <summary>
    /// 处理Html代码,并消除危险字符
    /// </summary>
    /// <param name="fString"></param>
    /// <returns></returns>
    public static string HTMLEncode(string fString)
    {

        if (fString != string.Empty)
        {
            ///替换尖括号
            fString = fString.Replace("<", "&lt;");
            fString = fString.Replace(">", "&rt;");
            ///替换引号
            fString = fString.Replace(((char)34).ToString(), "&quot;");
            fString = fString.Replace(((char)39).ToString(), "&#39;");
            ///替换空格
            fString = fString.Replace(((char)13).ToString(), "");
            ///替换换行符
            fString = fString.Replace(((char)10).ToString(), "<BR> ");

            fString = fString.Replace("\n", "<BR> ");

            //消除危险字符
            fString = fString.Replace("'", "''");
            fString = fString.Replace("?", "");
            //fString=fString.Replace("[","[[]");
            //fString = fString.Replace("%", "[%]");
            //fString = fString.Replace("-", "[-]");
            //fString = fString.Replace("_", "[_]");
            fString = fString.ToLower();
            //去除Select、Delete、Drop、Insert、Join、union等SQL关键字
            fString = fString.Replace("select", "");
            fString = fString.Replace("delete", "");
            fString = fString.Replace("drop", "");
            fString = fString.Replace("insert", "");
            fString = fString.Replace("join", "");
            fString = fString.Replace("union", "");
            fString = fString.Replace("java", "");
            fString = fString.Replace("javascript", "");
            fString = fString.ToUpper();
        }
        return (fString);
    }

    /// <summary>
    /// 还原html代码
    /// </summary>
    /// <param name="fString"></param>
    /// <returns></returns>
    public static string HTMLDecode(string fString)
    {
        if (fString != string.Empty)
        {
            ///替换尖括号
            fString = fString.Replace("&lt;", "<");
            fString = fString.Replace("&rt;", ">");
            ///替换引号
            fString = fString.Replace("&quot;", ((char)34).ToString());
            fString = fString.Replace("&#39;", ((char)39).ToString());
            ///替换空格
            //fString = fString.Replace("", ((char)13).ToString());
            ///替换换行符
            fString = fString.Replace("<BR> ", ((char)10).ToString());

            fString = fString.Replace("<BR>", " \n");
        }
        return (fString);
    }
}