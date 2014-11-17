namespace Selftaught.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;
    using System.Collections;
    using System.Data.Entity;

    using AutoMapper;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using Selftaught.Data.DataAccess;

    public abstract class KendoGridAdminController : AdminController
    {
        public KendoGridAdminController(ISelftaughtData data)
            : base(data)
        {
        }

        protected abstract IEnumerable GetData();

        protected abstract T GetById<T>(object id) where T : class;

        [HttpPost]
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var data = this.GetData()
                           .ToDataSourceResult(request);

            return this.Json(data);
        }

        [NonAction]
        protected virtual T Create<T>(object model) where T : class
        {
            if (model != null && this.ModelState.IsValid)
            {
                var dbModel = Mapper.Map<T>(model);
                this.ChangeEntityStateAndSave(dbModel, EntityState.Added);
                return dbModel;
            }

            return null;
        }

        [NonAction]
        protected virtual void Update<TModel, TViewModel>(TViewModel model, object id)
            where TModel : class
            where TViewModel : class
        {
            if (model != null && this.ModelState.IsValid)
            {
                var dbModel = this.GetById<TModel>(id);
                Mapper.Map<TViewModel, TModel>(model, dbModel);
                this.ChangeEntityStateAndSave(dbModel, EntityState.Modified);
            }
        }

        protected JsonResult GridOperation<T>(T model, [DataSourceRequest]
                                              DataSourceRequest request)
        {
            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        }

        private void ChangeEntityStateAndSave(object dbModel, EntityState state)
        {
            var entry = this.data.Context.Entry(dbModel);
            entry.State = state;
            this.data.SaveChanges();
        }
    }
}