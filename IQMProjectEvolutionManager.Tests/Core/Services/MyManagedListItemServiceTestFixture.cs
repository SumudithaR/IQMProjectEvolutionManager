using System;
using System.Linq;
using IQM.Common;
using IQM.Common.Domain;
using IQM.Common.Services;
using IQM.Common.Test.Core;
using IQMProjectEvolutionManager.Core.Enums;
using Ninject;
using NUnit.Framework;
using IQMProjectEvolutionManager.Core.Domain;

namespace IQMProjectEvolutionManager.Tests.Core.Services
{
    /// <summary>
    /// Drop down item service test fixutre.
    /// </summary>
    [TestFixture]
    public class MyManagedListItemServiceTestFixture : NHibernateTestFixtureBase
    {
        #region Data
        /// <summary>
        /// The list of items
        /// </summary>
        private readonly MyManagedListItem[] _items = new[]
            {
                new MyManagedListItem { Name = "Type 1", Order = 1, ShortName = "1", Visible = true, ListItemType = MyManagedListItemType.Paging },
                new MyManagedListItem { Name = "Type 2", Order = 2, ShortName = "2", Visible = true, ListItemType = MyManagedListItemType.Paging },
                new MyManagedListItem { Name = "Type 3", Order = 3, ShortName = "3", Visible = false, ListItemType = MyManagedListItemType.Paging },
                new MyManagedListItem { Name = "Type 4", Order = 4, ShortName = "4", Visible = true, ListItemType = MyManagedListItemType.Paging },
                new MyManagedListItem { Name = "Type 7", Order = 7, ShortName = "7", Visible = false, ListItemType = MyManagedListItemType.Paging }
            };
        #endregion

        /// <summary>
        /// Creates the initial data.
        /// </summary>
        public override void CreateInitialData()
        {

            var session = NHibernateHelper.GetSession();

            foreach (var item in _items)
            {
                session.Save(item);
            }
        }

        /// <summary>
        /// Determines whether this instance [can get visible by type].
        /// </summary>
        [Test]
        public void CanGetVisibleByType()
        {
            var dropDownItem = Kernel.Get<TypedListItemService<MyManagedListItem>>();
            var all = dropDownItem.GetAll();
            var first = all.First();
            var d = dropDownItem.GetVisible(first.ListItemType);
            
            for (int i = 0; i < d.Count; i++)
            {
                Assert.IsNotNull(d.ElementAt(i));
            }
            Assert.AreEqual(2, d.Count);
        }

        /// <summary>
        /// Nons the visible not returned.
        /// </summary>
        [Test]
        public void NonVisibleNotReturned()
        {
            var dropDownItem = Kernel.Get<TypedListItemService<MyManagedListItem>>();
            var all = dropDownItem.GetAll();
            var second = all.ElementAt(2);
            var d = dropDownItem.GetVisible(second.ListItemType);

            Assert.IsEmpty(d);
            Assert.AreEqual(0, d.Count);
        }

        /// <summary>
        /// Determines whether this instance [can get by id].
        /// </summary>
        [Test]
        public void CanGetById()
        {
            var dropDownItem = Kernel.Get<TypedListItemService<MyManagedListItem>>();
            long amountOfDropDownItems = dropDownItem.GetAll().Count;
            for (long i = 1; i < amountOfDropDownItems; i++)
            {
                Assert.IsNotNull(dropDownItem.GetById(i));
            }
        }

        /// <summary>
        /// Determines whether this instance [can not get by id].
        /// </summary>
        [Test]
        public void CanNotGetById()
        {
            var dropDownItem = Kernel.Get<TypedListItemService<MyManagedListItem>>();
            long amountOfDropDownItems = dropDownItem.GetAll().Count;
            Assert.IsNull(dropDownItem.GetById(amountOfDropDownItems + 1));
        }

        /// <summary>
        /// Determines whether this instance can save.
        /// </summary>
        [Test]
        public void CanSave()
        {
            var dropDownItem = Kernel.Get<TypedListItemService<MyManagedListItem>>();
            long amountOfDropDownItems = dropDownItem.GetAll().Count;

            dropDownItem.Save(new MyManagedListItem
            {
                Name = "Type 5",
                Order = 5,
                ShortName = "5",
                Visible = true,
                ListItemType = MyManagedListItemType.Paging
            });

            Assert.IsNotNull(dropDownItem.GetById(amountOfDropDownItems + 1));
        }

        /// <summary>
        /// Determines whether this instance can remove.
        /// </summary>
        [Test]
        public void CanRemove()
        {
            var dropDownItem = Kernel.Get<TypedListItemService<MyManagedListItem>>();
            long amountOfDropDownItems = dropDownItem.GetAll().Count;

            var newDropDownItem = new MyManagedListItem
            {
                Name = "Type 6",
                Order = 6,
                ShortName = "6",
                Visible = true,
                ListItemType = MyManagedListItemType.Paging
            };

            dropDownItem.Save(newDropDownItem);
            Assert.IsNotNull(dropDownItem.GetById(amountOfDropDownItems + 1));

            dropDownItem.Remove(newDropDownItem);
            Assert.IsNull(dropDownItem.GetById(amountOfDropDownItems + 1));
        }

        /// <summary>
        /// Determines whether this instance [can get all].
        /// </summary>
        [Test]
        public void CanGetAll()
        {
            var dropDownItem = Kernel.Get<TypedListItemService<MyManagedListItem>>();
            var amountOfDropDownItems = dropDownItem.GetAll().Count;
            Assert.AreEqual(amountOfDropDownItems, _items.Length);
        }

        /// <summary>
        /// Determines whether this instance [can get visible].
        /// </summary>
        [Test]
        public void CanGetVisible()
        {
            var dropDownItem = Kernel.Get<TypedListItemService<MyManagedListItem>>();
            var itemType = MyManagedListItemType.Paging;
            var visible = dropDownItem.GetVisible(itemType);
            int count = 0;
            foreach (var item in _items)
            {
                if (item.Visible && (MyManagedListItemType)Enum.Parse(typeof(MyManagedListItemType), item.ListItemType.ToString()) == itemType)
                {
                    count++;
                }
            }

            Assert.AreEqual(visible.Count, count);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        public override void TearDown()
        {
            //foreach (var dropDownItem in _items)
            //{
            //    dropDownItem.ManagedListItemId = 0;
            //}

            base.TearDown();
        }
    }
}
