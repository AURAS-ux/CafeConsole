using Cafe.Domain.Beverages;
using Cafe.Domain.Beverages.Decorators;
using Cafe.Domain.Events;
using Cafe.Domain.Extentions;
using Cafe.Domain.Factories;
using Cafe.Domain.Order;
using Cafe.Domain.Pricing;

namespace Cafe.Application.Services
{
    public class BeverageService : IBeverageService
    {
        private IBeverageFactory _beverageFactory;
        private IPricingStrategy? _pricingStrategy;
        private Receipt _receipt = new Receipt();

        private IBeverage? _beverage;
        private IOrderEventPublisher _publisher;

        public BeverageService(IBeverageFactory beverageFactory, IOrderEventPublisher publisher)
        {
            _beverageFactory = beverageFactory;
            _receipt.OrderId = Guid.NewGuid();
            _receipt.At = DateTime.Now;
            _publisher = publisher;
        }

        public decimal ApplyPricing()
        {
            if(_beverage == null)
            {
                throw new InvalidOperationException("Beverage must be served before applying pricing.");
            }
            if(_pricingStrategy == null)
            {
                throw new InvalidOperationException("Pricing strategy must be set before applying pricing.");
            }
            _receipt.Subtotal = _beverage.Cost();
            decimal total = _pricingStrategy.Apply(_beverage.Cost());
            _receipt.Total = total;
            return total;
        }

        public void Customize(List<string> addOns)
        {
            if(_beverage == null)
            {
                throw new InvalidOperationException("Beverage must be served before customization.");
            }
            if(addOns == null || addOns.Count == 0)
            {
                return;
            }
            foreach (var addOn in addOns)
            {
                _beverage = addOn.ToLower() switch
                {
                    "milk" => new MilkDecorator(_beverage),
                    "syrup" => new SyrupDecorator(_beverage,"vanills"), //TODO: adauga mai multe arome
                    "extrashot" => new ExtraShotDecorator(_beverage),
                    _ => _beverage
                };
                _receipt.Items.Add(addOn);
            }
        }

        public Receipt IssueReceipt()
        {
            _publisher.Published(
                new OrderPlaced(_receipt.OrderId, _receipt.At, string.Join(", ", _receipt.Items.Select(i => i)), _receipt.Subtotal, _receipt.Total)
                );
            return _receipt;
        }

        public void Serve(string beverageType)
        {
            _receipt.Items.Clear();
            _beverage = _beverageFactory.CreateBeverage(beverageType);
            _receipt.Items.Add(_beverage.Name);
        }

        public void SetPricingStrategy(PricingStrategy strategy)
        {
            _pricingStrategy = PricingStrategyManager.GetStrategy(strategy);
            _receipt.Pricing = _pricingStrategy.GetName();
        }
    }
}
