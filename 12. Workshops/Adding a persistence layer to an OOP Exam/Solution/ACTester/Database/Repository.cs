namespace ACTester.Database
{
    using System.Collections.Generic;
    using AcTester.Helpers.Utilities;
    using Exceptions;
    using Interfaces;

    public class Repository<T> : IRepository<T> where T : IManufacturable, IModelable
    {
        public Repository()
        {
            this.ItemsByManufacturerAndModel = new Dictionary<string, T>();
        }

        public int Count { get; protected set; }

        protected Dictionary<string, T> ItemsByManufacturerAndModel { get; set; }

        public virtual void Add(T item)
        {
            var manufacturerAndModel = item.Manufacturer + item.Model;
            if (this.ItemsByManufacturerAndModel.ContainsKey(manufacturerAndModel))
            {
                throw new DuplicateEntryException(Constants.DuplicateEntry);
            }

            this.ItemsByManufacturerAndModel.Add(manufacturerAndModel, item);
            this.Count++;
        }

        public virtual T GetItem(string manufacturer, string model)
        {
            if (!this.ItemsByManufacturerAndModel.ContainsKey(manufacturer + model))
            {
                throw new NonExistantEntryException(Constants.NonExistantEntry);
            }

            return this.ItemsByManufacturerAndModel[manufacturer + model];
        }
    }
}
