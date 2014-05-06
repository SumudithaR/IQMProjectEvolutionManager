using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
//using AssetManagement.Core.Domain;
//using AssetManagement.Core.Enums;
//using AssetManagement.Core.Services;
using IQM.Common.Web.Utility;
using IQM.Common.Web.ViewModels;
using IQM.Common.Web.Controllers;

namespace AssetManagement.Controllers
{
    /// <summary>
    /// Controller to manage Administration of dynamic drop down lists.
    /// </summary>
    [TocAuthorize]
    public class ManagedListItemController : TypedListItemController<MyManagedListItem>
    {

    }
}