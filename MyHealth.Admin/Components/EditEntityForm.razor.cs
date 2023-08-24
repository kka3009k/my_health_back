using Microsoft.AspNetCore.Components;
using MyHealth.Data.Entities;

namespace MyHealth.Admin.Components
{
    public partial class EditEntityForm<TEntity> where TEntity : EntityBase
    {
        [Parameter]
        public RenderFragment<TEntity> Fields { get; set; }

        [Parameter]
        public EventCallback OnSave { get; set; }

        [Parameter]
        public EventCallback OnCancel { get; set; }

        private bool _isEdit;

        private TEntity _item;

        public void StartEdit(TEntity pEditItem)
        {
            _item = pEditItem;
            _isEdit = true;
        }

        private void SaveClick()
        {
            _isEdit = false;
            OnSave.InvokeAsync();
        }

        private void CancelClick()
        {
            _isEdit = false;
            OnCancel.InvokeAsync();
        }
    }
}
