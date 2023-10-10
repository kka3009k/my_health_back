using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using MyHealth.Admin.Services;
using MyHealth.Data;
using MyHealth.Data.Entities;
using Radzen;
using Radzen.Blazor;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Runtime.CompilerServices;

namespace MyHealth.Admin.Components
{
    public partial class ListForm<TEntity> where TEntity : EntityBase
    {
        [Parameter]
        public RenderFragment Columns { get; set; }

        [Parameter]
        public EventCallback<TEntity> OnEdit { get; set; }

        [Inject]
        private IDbContextFactory<MyDbContext> _dbFactory { get; set; }

        [Inject]
        private SpinnerService _spinner { get; set; }

        [Inject]
        private DialogService _dialogService { get; set; }

        private MyDbContext _db;

        private RadzenDataGrid<TEntity> _grid { get; set; }

        private int _count { get; set; }

        private List<TEntity> _items { get; set; }

        private bool _isEdit;

        private TEntity _selectedItem;

        private string _searchString;

        protected override void OnInitialized()
        {
            _db = _dbFactory.CreateDbContext();
        }

        private async Task LoadData(LoadDataArgs args)
        {
            await _spinner.RunAsync(async () =>
            {
                // This demo is using https://dynamic-linq.net
                var query = _db.Set<TEntity>().AsNoTracking();

                if (!string.IsNullOrEmpty(args.Filter))
                {
                    // Filter via the Where method
                    query = query.Where(args.Filter);
                }

                if (!string.IsNullOrEmpty(args.OrderBy))
                {
                    // Sort via the OrderBy method
                    query = query.OrderBy(args.OrderBy);
                }

                // Important!!! Make sure the Count property of RadzenDataGrid is set.
                _count = query.Count();

                // Perform paging via Skip and Take.
                _items = await query.Skip(args.Skip.Value).Take(args.Top.Value).ToListAsync();
            });
        }

        private void Insert()
        {
            _selectedItem = Activator.CreateInstance<TEntity>();
            StartEdit();
        }

        private async Task Edit(TEntity pItem)
        {
            await _spinner.RunAsync(async () =>
             {
                 _selectedItem = await _db.Set<TEntity>().FirstOrDefaultAsync(f => f.ID == pItem.ID);
                 StartEdit();
             });
        }

        private void StartEdit()
        {
            _isEdit = true;
            OnEdit.InvokeAsync(_selectedItem);
        }

        public async Task Save()
        {
            await _spinner.RunAsync(async () =>
              {
                  if (_selectedItem.ID == default)
                      await _db.AddAsync(_selectedItem);

                  await _db.SaveChangesAsync();

                  await EndEdit();
              });
        }

        public async Task EndEdit()
        {
            _isEdit = false;
            await _grid.Reload();
        }

        private async Task OnSearch(string pText)
        {
            _searchString = pText;
            await _grid.Reload();
        }

        private async Task OnDeleteClick(TEntity pItem)
        {
            var result = await _dialogService.Confirm("Вы уверенны что хотите удалить запись?", "Подтверждение", new ConfirmOptions { OkButtonText = "Да", CancelButtonText = "Нет" });

            if (result == true)
            {
                await _spinner.RunAsync(async () => await _db.Set<TEntity>().Where(w => w.ID == pItem.ID).ExecuteDeleteAsync());
                await _grid.Reload();
            }
        }
    }
}
