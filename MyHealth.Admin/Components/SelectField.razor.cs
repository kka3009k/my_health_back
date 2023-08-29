using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using MyHealth.Admin.Services;
using MyHealth.Common;
using MyHealth.Data;
using MyHealth.Data.Entities;
using Radzen;
using System.Linq.Dynamic.Core;
using System.Runtime.CompilerServices;

namespace MyHealth.Admin.Components
{
    public partial class SelectField<TEntity, TValue> where TEntity : EntityBase
    {
        [Parameter]
        public TValue Value { get; set; }

        [Parameter]
        public EventCallback<TValue> ValueChanged { get; set; }

        [Parameter]
        public string TextProperty { get; set; }

        [Parameter]
        public string ValueProperty { get; set; } = nameof(EntityBase.ID);

        [Inject]
        private IDbContextFactory<MyDbContext> _dbFactory { get; set; }

        [Inject]
        private SpinnerService _spinner { get; set; }

        private MyDbContext _db;

        private int _count { get; set; }

        private List<TEntity> _items { get; set; }

        private TEntity _selectedValue;

        protected override void OnInitialized()
        {
            _db = _dbFactory.CreateDbContext();

            if (Value != null && ValueUtil.GetType(typeof(TValue)) == typeof(int))
            {
                _spinner.Run(() =>
               {
                   var id = Value as int?;
                   _selectedValue = _db.Set<TEntity>().FirstOrDefault(f => f.ID == id);
               });
            }
        }

        private async Task LoadData(LoadDataArgs args)
        {
            await _spinner.RunAsync(async () =>
             {
                 var query = _db.Set<TEntity>().AsNoTracking();

                 if (!string.IsNullOrEmpty(args.Filter))
                 {
                     // query = query.Where(c => c.CustomerID.ToLower().Contains(args.Filter.ToLower()) || c.ContactName.ToLower().Contains(args.Filter.ToLower()));
                 }

                 _count = query.Count();

                 if (!string.IsNullOrEmpty(args.OrderBy))
                 {
                     query = query.OrderBy(args.OrderBy);
                 }

                 if (args.Skip != null)
                 {
                     query = query.Skip(args.Skip.Value);
                 }

                 if (args.Top != null)
                 {
                     query = query.Take(args.Top.Value);
                 }

                 _items = await query.ToListAsync();
             });
        }
    }
}
