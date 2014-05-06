using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AssetManagement.Core.Domain;
using AssetManagement.Core.Enums;
using AssetManagement.Core.Services;
using IQM.Common.Web.Utility;
using IQM.Common.Web.ViewModels;

namespace AssetManagement.Controllers
{
    /// <summary>
    /// Controller to manage Administration of dynamic drop down lists.
    /// </summary>
    [TocAuthorize]
    public class ManagedListItemController : Controller
    {
        #region Members
        /// <summary>
        /// Gets or sets the drop down item Service.
        /// </summary>
        /// <value>
        /// The drop down item Service.
        /// </value>
        public ManagedListItemService ManagedListItemService { get; set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagedListItemController"/> class.
        /// </summary>
        public ManagedListItemController()
        {
            ManagedListItemService = DependencyResolver.Current.GetService<ManagedListItemService>();
        } 
        #endregion

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>Index View</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Edits the specified type.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// Edit View
        /// </returns>
        public ActionResult Edit(ManagedListItemType id)
        {
            var dropListViewModel = new Core.ViewModels.DropDownItemViewModel
                {
                    Data = ManagedListItemService.GetAll().Where(x => x.ManagedListItemType == id).OrderBy(x => x.Order).ToList(),
                    ManagedListItemType = id
                };
            return View(dropListViewModel);
        }

        /// <summary>
        /// Edits the specified type.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="dropDownItems">The drop down items.</param>
        /// <returns>
        /// Redirects to edit view
        /// </returns>
        [HttpPost]
        public ActionResult Edit(ManagedListItemType id, IList<ManagedListItem> dropDownItems)
        {
            foreach (var dropDownItem in dropDownItems)
            {
                dropDownItem.ManagedListItemType = id;
                ManagedListItemService.Save(dropDownItem);
                TempData["status"] = StatusMessage.Success("Successfully updated drop down list");
            }

            return RedirectToAction("Edit", new { id });
        } 

        /// <summary>
        /// Adds the type of the barrier work.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="managedListItem">The drop down item.</param>
        /// <returns>
        /// Json Result of Save
        /// </returns>
        [HttpPost]
        public ActionResult Create(ManagedListItemType id, ManagedListItem managedListItem)
        {
            if (TryUpdateModel(managedListItem))
            {
                managedListItem.ManagedListItemType = id;
                ManagedListItemService.Save(managedListItem);
            }

            return RedirectToAction("Edit", new { id });
        } 

        /// <summary>
        /// Removes the type of the barrier work.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Json Result of action</returns>
        [HttpPost]
        public JsonResult Remove(long id)
        {
            try
            {
                var barrierWorkType = ManagedListItemService.GetById(id);
                ManagedListItemService.Remove(barrierWorkType);

                return Json(
                    new
                    {
                        success = true,
                        message = "Item Deleted"
                    });
            }
            catch (Exception exception)
            {
                return Json(
                    new
                    {
                        success = false,
                        message = exception.Message
                    });
            }
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The json result for the matching items</returns>
        public JsonResult GetItems(string type)
        {
            JsonResult result;
            try
            {
                var itemType = (ManagedListItemType)Enum.Parse(typeof(ManagedListItemType), type);
                result = Json(ManagedListItemService.GetVisible(itemType).OrderBy(p => p.Name), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                result = Json(new List<ManagedListItem> { new ManagedListItem { Name = ex.Message } } , JsonRequestBehavior.AllowGet);
            }

            return result;
        }
    }
}