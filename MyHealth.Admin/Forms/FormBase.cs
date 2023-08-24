using Microsoft.AspNetCore.Components;
using MyHealth.Admin.Components;
using MyHealth.Data.Entities;

namespace MyHealth.Admin.Forms
{
    public class FormBase<TEntity> : ComponentBase where TEntity : EntityBase
    {
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
