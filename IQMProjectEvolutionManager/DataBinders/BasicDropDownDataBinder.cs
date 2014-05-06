using System;
using System.Linq;
using System.Web.Mvc;
using IQM.Common.Interfaces;
using MetricsMaster.Core.Domain;
using MetricsMaster.Core.Services;

namespace MetricsMaster.DataBinders
{
    public class BasicDropDownDataBinder<T, K> : DefaultModelBinder where K : IViewModel<T>
    {
        /// <summary>
        /// Binds the model by using the specified controller context and binding context.
        /// </summary>
        /// <param name="controllerContext">The context within which the controller operates. The context information includes the controller, HTTP content, request context, and route data.</param>
        /// <param name="bindingContext">The context within which the model is bound. The context includes information such as the model object, model name, model type, property filter, and value provider.</param>
        /// <returns>
        /// The bound object.
        /// </returns>
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var viewModel = (K)bindingContext.Model;

            if (viewModel != null && viewModel.Data != null)
            {
                // Bind all the drop down items
                foreach (var propertyInfo in viewModel.Data.GetType().GetProperties().Where(prop => prop.PropertyType == typeof(DropDownItem)))
                {
                    if (bindingContext.ValueProvider.GetValue(String.Format("Data.{0}.DropDownItemId", propertyInfo.Name)) != null)
                    {
                        var ddiId = bindingContext.ValueProvider.GetValue(String.Format("Data.{0}.DropDownItemId", propertyInfo.Name)).ConvertTo(typeof(long));
                        var ddiService = DependencyResolver.Current.GetService<DropDownItemService>();
                        propertyInfo.SetValue(viewModel.Data, ddiService.GetById(ddiId), null);
                    }
                }
            }

            return (K)base.BindModel(controllerContext, bindingContext);
        }
    }
}