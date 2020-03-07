using System.ComponentModel;

namespace Michalski.Utils
{
    public class ExtBindingList<T> : BindingList<T>
    {
        public ExtBindingList() : base()
        {
            this.AllowNew = true;
            this.AllowEdit = true;
            this.AllowRemove = true;
        }
        protected override void RemoveItem(int index)
        {
            T deletedItem = this.Items[index];
            BeforeChange?.Invoke(deletedItem);
            base.RemoveItem(index);
        }

        public delegate void ItemChangedDelegate(T item);
        public event ItemChangedDelegate BeforeChange;
    }
}
