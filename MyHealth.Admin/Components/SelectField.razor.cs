using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using MyHealth.Admin.Services;
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
        public string ValueProperty { get; set; }

        [Inject]
        private IDbContextFactory<MyDbContext> _dbFactory { get; set; }

        [Inject]
        private SpinnerService _spinner { get; set; }

        private MyDbContext _ctx;

        private int _count { get; set; }

        private List<TEntity> _items { get; set; }

        protected override void OnInitialized()
        {
            _ctx = _dbFactory.CreateDbContext();
        }

        private async Task LoadData(LoadDataArgs args)
        {
            await _spinner.RunAsync(async () =>
             {
                 var query = _ctx.Set<TEntity>().AsNoTracking();

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
