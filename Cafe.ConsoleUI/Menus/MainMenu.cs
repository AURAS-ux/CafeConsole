using Cafe.Application.Services;
using Cafe.Domain.Events;
using Cafe.Domain.Extentions;
using Cafe.Domain.Pricing;
using Cafe.Infrastructure.Observers;

namespace Cafe.ConsoleUI.Menus
{
    internal class MainMenu
    {
        private IBeverageService _beverageService;
        private IOrderEventSubscriber _orderEventSubscriber;

        public MainMenu(IBeverageService beverageService, IOrderEventSubscriber orderEventSubscriber)
        {
            _beverageService = beverageService;
            _orderEventSubscriber = orderEventSubscriber;
        }

        private void Start()
        {
            Console.WriteLine("=== Coffee Console Started ===");
            Console.WriteLine();
            Console.WriteLine("Please Provide your order details");
        }

        public void Run()
        {
            bool running = true;
            do
            {
                Start();
                SelectBeverage();
                SelectAddOns();
                SelectPricing();
                PrintReceipt();
                running = RestartProcess();
            }
            while (running);
            End();
        }

        private void End()
        {
            Console.WriteLine("=== Coffee Console Ended ===");
            if(_orderEventSubscriber is InMemoryOrderAnalytics analytics)
            {
                Console.WriteLine($"=== Analitycs for ${DateTime.Now} ===");
                Console.WriteLine($"Total Orders: {analytics.OrdersCount}");
                Console.WriteLine($"Total Revenue: ${analytics.Revenue}");
                Console.WriteLine("===/===");
            }
            Console.WriteLine();
        }

        private bool RestartProcess()
        {
            Console.WriteLine("=== Order finished ===");
            Console.WriteLine("Do you want to order something else?");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");
            Console.WriteLine("===/===");
            string input = Utils.RequestInput();
            switch (input)
            {
                case "1":
                    return true;
                case "2":
                    return false;
                default:
                    Console.WriteLine("Invalid selection. Exiting.");
                    return false;
            }
        }

        private void PrintReceipt()
        {
            Console.WriteLine("=== Printing Receipt ===");
            var receipt = _beverageService.IssueReceipt();
            Console.WriteLine(receipt.Display());
            Console.WriteLine("===/===");
        }

        private void SelectPricing()
        {
            Console.WriteLine("=== Select Pricing Type ===");
            Console.WriteLine();
            Console.WriteLine("1. Regular");
            Console.WriteLine("2. Happy Hour");
            Console.WriteLine("===/===");
            bool running = true;
            do
            {
                string input = Utils.RequestInput();
                switch (input)
                {
                    case "1":
                        _beverageService.SetPricingStrategy(PricingStrategy.Regular);
                        running = false;
                        break;
                    case "2":
                        _beverageService.SetPricingStrategy(PricingStrategy.HappyHour);
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }
            }
            while (running);

            var subtotal = _beverageService.ApplyPricing(); //TODO: Use subtotal
        }

        private void SelectAddOns()
        {
            Console.WriteLine("=== Available AddOns ===");
            Console.WriteLine();
            Console.WriteLine("1. Milk(+0.40)");
            Console.WriteLine("2. Syrup(+0.50)");
            Console.WriteLine("3. Extra Shot(+0.80)");
            Console.WriteLine("0. Done");
            Console.WriteLine("===/===");
            bool running = true;
            List<string> selectedAddOns = new List<string>();
            do
            {
                string input = Utils.RequestInput();
                switch (input)
                {
                    case "1":
                        selectedAddOns.Add("milk");
                        break;
                    case "2":
                        selectedAddOns.Add("syrup");
                        break;
                    case "3":
                        selectedAddOns.Add("extrashot");
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }
            }
            while (running);

            _beverageService.Customize(selectedAddOns);
        }

        private void SelectBeverage()
        {
            Console.WriteLine("=== Available Beverages ===");
            Console.WriteLine();
            Console.WriteLine("1. Espresso (base $2.50)");
            Console.WriteLine("2. Tea (base $2.00)");
            Console.WriteLine("3. Hot Chocolate (base $3.00)");
            Console.WriteLine("===/===");
            Console.WriteLine();
            bool running = true;
            do
            {
                string input = Utils.RequestInput();
                switch (input)
                {
                    case "1":
                        _beverageService.Serve("espresso");
                        running = false;
                        break;
                    case "2":
                        _beverageService.Serve("tea");
                        running = false;
                        break;
                    case "3":
                        _beverageService.Serve("hotchocolate");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }
            }
            while (running);
        }
    }
}
