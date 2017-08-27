using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevelopersHub.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Routing;
public class BaseController : Controller
{


    public ViewModel ViewModel { get; set; }
    public GenericUnitOfWork<DevelopersHubContext> UnitOfWork { get; set; }

    protected MiscParams MiscParams { get; set; }

    protected RouteParams RouteParams { get; set; }
    protected QueryParametersList QueryParametersList { get; set; }
    protected RouteParamsToQueryParametersMappingsList RouteParamsToQueryParametersMappingsList { get; set; }

    private int _loggedInUserId = 1;
    protected int LoggedInUserId { get { return _loggedInUserId; } set { _loggedInUserId = value; } }

    protected BaseController()
    {
        this.UnitOfWork = new DevelopersHub.Models.DevelopersHubUnitOfWork();
    }
    Microsoft​.AspNetCore​.Http.IHttpContextAccessor _httpContextAccessor;
    Microsoft.AspNetCore.Hosting.IHostingEnvironment _HostingEnvironment;

    protected BaseController(Microsoft​.AspNetCore​.Http.IHttpContextAccessor httpContextAccessor, Microsoft.AspNetCore.Hosting.IHostingEnvironment HostingEnvironment):base()
    {
        _httpContextAccessor = httpContextAccessor;
        _HostingEnvironment = HostingEnvironment;
        
    }

    public string GetRouteValue(string routeParameter)
    {
        return (string)_httpContextAccessor.HttpContext.GetRouteValue(routeParameter).ToString().ToLower();
    }
    protected virtual void SetRouteParams()
        {

        }
        protected virtual void SetQueryParametersList()
        {

        }
        protected virtual void FillQueryParametersListFromMappings()
        {

            foreach (KeyValuePair<string, RouteParamsToQueryParametersMappings> items in RouteParamsToQueryParametersMappingsList)
            {
                foreach (RouteParamsToQueryParametersMapping item in items.Value)
                {
                    if (item.ParamType == ParamType.RouteParam)
                    {
                        if (RouteParams[item.ParamName].isSet)
                        {
                            QueryParameters __QueryParameters = QueryParametersList[items.Value.QueryName];

                            __QueryParameters.Where = "";
                            __QueryParameters.SqlParams = new List<System.Data.SqlClient.SqlParameter>();
                            __QueryParameters.Where = item.ColumnName + " " + item.Operation + " " +
                                string.Format(item.Format,
                                "@" + item.ColumnName);
                            SqlParameter _sqlParam = new SqlParameter();
                            _sqlParam.ParameterName = "@" + item.ColumnName;
                            _sqlParam.Value = RouteParams[item.ParamName].value;
                            __QueryParameters.SqlParams.Add(_sqlParam);

                            QueryParametersList.Add(__QueryParameters);
                        }
                    }
                    else if (item.ParamType == ParamType.MiscParam)
                    {
                        QueryParameters __QueryParameters = QueryParametersList[items.Value.QueryName];
                        __QueryParameters.QueryName = items.Value.QueryName;
                        __QueryParameters.Where = "";
                        __QueryParameters.SqlParams = new List<System.Data.SqlClient.SqlParameter>();
                        __QueryParameters.Where = item.ColumnName + " " + item.Operation + " " +
                            string.Format(item.Format,
                            "@" + item.ColumnName);
                        SqlParameter _sqlParam = new SqlParameter();
                        _sqlParam.ParameterName = "@" + item.ColumnName;
                        _sqlParam.Value = MiscParams[item.ParamName].value;
                        __QueryParameters.SqlParams.Add(_sqlParam);

                        QueryParametersList.Add(__QueryParameters);
                    }
                }

            }

        }
        protected virtual void SetRouteParamsToQueryParametersMappings()
        {


        }

        protected virtual void FillMiscParams()
        {
            MiscParams.LoggedInUserId = LoggedInUserId;
        }

        protected virtual void FillViewData()
        {
            ;
        }
   
    public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            MiscParams = new MiscParams();
            RouteParams = new RouteParams();
            QueryParametersList = new QueryParametersList();
            RouteParamsToQueryParametersMappingsList = new RouteParamsToQueryParametersMappingsList();

            FillMiscParams();

            SetRouteParams();

            FillRouteParamsFromPath();

            SetQueryParametersList();

            SetRouteParamsToQueryParametersMappings();

            FillQueryParametersListFromMappings();

        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ViewData["MiscParams"] = MiscParams;
            
        if(ViewModel!= null)
        {
            ViewModel.HttpContextAccessor = _httpContextAccessor;
        }
            

            FillViewData();
        }
        private void FillRouteParamsFromPath()
        {
        //string __path = ControllerContext.HttpContext.Request.RawUrl;
        string __path = ControllerContext.HttpContext.Request.GetDisplayUrl();

        //for (int i = 0; i < ControllerContext.HttpContext.Request.QueryString.Count; i++)
        for (int i = 0; i < ControllerContext.HttpContext.Request.Query.Count; i++)
        {

                string __queryParamName = "";
            //__queryParamName = ControllerContext.HttpContext.Request.QueryString.GetKey(i);
            List<string> __list_queryparamnames = ControllerContext.HttpContext.Request.Query.Keys.ToList();

            __queryParamName = __list_queryparamnames[i];
            if (RouteParams.ContainsKey(__queryParamName))
                {
                    //string __queryParamValue = "";
                    Microsoft.Extensions.Primitives.StringValues ____queryParamValue;
                    //__queryParamValue = ControllerContext.HttpContext.Request.QueryString[i];
                    ControllerContext.HttpContext.Request.Query.TryGetValue(__queryParamName, out ____queryParamValue);
                    RouteParams[__queryParamName]._value = ____queryParamValue[0];
                }
            }

            if (!string.IsNullOrEmpty(__path) && __path != "/")
            {
                RouteParams.ExtractParamsFromPathString(__path, '/');
            }
        }
    }
    public enum ParamType { RouteParam = 1, MiscParam = 2 }
    public class RouteParamsToQueryParametersMapping
    {
        public ParamType ParamType { get; set; }
        public string ParamName { get; set; }
        public string ColumnName { get; set; }
        public string Operation { get; set; }
        private string _format = "{0}";
        public string Format { get { return _format; } set { _format = value; } }


    }
    public class RouteParamsToQueryParametersMappings : List<RouteParamsToQueryParametersMapping>
    {
        public string QueryName { get; set; }
    }
    public class RouteParamsToQueryParametersMappingsList : Dictionary<string, RouteParamsToQueryParametersMappings>
    {
        public new void Add(RouteParamsToQueryParametersMappings RouteParamsToQueryParametersMappings)
        {
            base.Add(RouteParamsToQueryParametersMappings.QueryName, RouteParamsToQueryParametersMappings);
        }

    }
    public class MiscParam
    {
        public string name { get; set; }
        public string defaultValue { get; set; }
        public string _value { get; set; }
        public string value
        {
            get
            {
                return _value != null ? _value : defaultValue;
            }
            set
            {
                _value = value;
            }
        }
        public bool isSet
        {
            get
            {
                if (!string.IsNullOrEmpty(_value))
                {
                    return true;
                }
                return false;
            }
        }
        public virtual List<string> validSet
        {
            get;
            set;

        }
    }
    public sealed class MiscParams : Dictionary<string, MiscParam>
    {

        public int LoggedInUserId
        {
            get
            {

                return Convert.ToInt32(this["LoggedInUserId"].value.ToString());
            }
            set
            {
                if (!this.ContainsKey("LoggedInUserId"))
                {
                    this["LoggedInUserId"] = new MiscParam();

                }


                this["LoggedInUserId"].value = value.ToString();
            }
        }

    }
    public sealed class QueryParameters
    {

        public string QueryName { get; set; }
        private int _pagesize = 1;
        public int PageSize { get { return _pagesize; } set { _pagesize = value; } }
        private int _pageindex = 1;
        public int PageIndex { get { return _pageindex; } set { _pageindex = value; } }
        public int PageCount { get; set; }

        private string _orderby = "[id]";
        public string OrderBy { get { return _orderby; } set { _orderby = value; } }
        private string _where = "";
        public string Where { get { return _where; } set { _where = value; } }

        private List<SqlParameter> _sqlParams = new List<SqlParameter>();
        public List<SqlParameter> SqlParams { get { return _sqlParams; } set { _sqlParams = value; } }


        public string SqlSourceObjectName { get; set; }


    }
    public sealed class QueryParametersList : Dictionary<string, QueryParameters>
    {
        public new void Add(QueryParameters QueryParameters)
        {
            this[QueryParameters.QueryName] = QueryParameters;
        }
        public new QueryParameters this[string Name]
        {
            get
            {
                if (!base.ContainsKey(Name))
                {
                    QueryParameters __queryParameters = new QueryParameters();
                    __queryParameters.QueryName = Name;
                    Add(__queryParameters);

                }
                return base[Name];
            }

            set
            {
                base[Name] = value;

            }
        }
    }
    public class RouteParam
    {
        public string name { get; set; }
        public string defaultValue { get; set; }
        public string _value { get; set; }
        public string value
        {
            get
            {
                return _value != null ? _value : defaultValue;
            }
            set
            {
                _value = value;
            }
        }
        public bool isSet
        {
            get
            {
                if (!string.IsNullOrEmpty(_value))
                {
                    return true;
                }
                return false;
            }
        }
        public virtual List<string> validSet
        {
            get;
            set;

        }
    }
    public sealed class RouteParams : Dictionary<string, RouteParam>
    {
        public void ExtractParamsFromPathString(string path, char sepChar)
        {
            string __path = "";
            if (path.IndexOf('?') != -1)
                __path = path.Substring(0, path.IndexOf('?'));
            else
                __path = path;
            string[] __arr = __path.Split(new char[] { '/' });
            for (int i = 0; i < __arr.Length; i++)
            {
                if (i % 2 == 1 && this.Keys.Contains(__arr[i]))
                {
                    if (this[__arr[i]].validSet == null || this[__arr[i]].validSet.Contains(__arr[i + 1]))
                    {
                        this[__arr[i]]._value = __arr[i + 1];
                    }
                    else
                    {
                        throw new
                            LSNDException(
                            "LSNDExceptionTypes." + LSNDExceptionTypes.InvalidRouteParam + "(" + (int)LSNDExceptionTypes.InvalidRouteParam + ")",
                            LSNDExceptionTypes.InvalidRouteParam);
                    }
                }
            }


        }
    }
    public class LSNDException : System.Exception
    {
        public LSNDExceptionTypes LSNDExceptionTypes
        {
            get;
            set;
        }
        public LSNDException(LSNDExceptionTypes lsndExceptionTypes)
            : base()
        {
            LSNDExceptionTypes = lsndExceptionTypes;
        }
        public LSNDException(string Message, LSNDExceptionTypes lsndExceptionTypes)
            : base(Message)
        {
            LSNDExceptionTypes = lsndExceptionTypes;
        }

        public LSNDException(string Message, LSNDExceptionTypes lsndExceptionTypes, Exception innerException)
            : base(Message, innerException)
        {
            LSNDExceptionTypes = lsndExceptionTypes;
        }
    }
    public enum LSNDExceptionTypes
    {
        InvalidRouteParam = 1
    }


