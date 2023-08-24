using Microsoft.AspNetCore.Components;
using MyHealth.Admin.Components;
using MyHealth.Data.Entities;

namespace MyHealth.Admin.Forms
{
    public partial class Form<TEntity> where TEntity : EntityBase
    {
        [Parameter]
        public RenderFragment Columns { get; set; }

        [Parameter]
        public RenderFragment<TEntity> Fields { get; set; }

        private protected ListForm<TEntity> _listForm;
        private protected EditEntityForm<TEntity> _editForm;

        private protected void OnEditClick(TEntity pItem)
        {
            _editForm.StartEdit(pItem);
        }

        private protected async Task OnSaveClick()
        {
            await _listForm.Save();
        }

        private protected async Task OnCancelClick()
        {
            await _listForm.EndEdit();
        }
    }
}
