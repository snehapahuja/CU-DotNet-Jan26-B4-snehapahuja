using System.Linq.Expressions;

namespace FinanceMngmt
{
    namespace FinancialPortfolioSystem
    {
        public class InvalidFinancialDataException : Exception
        {
            public InvalidFinancialDataException(string message) : base(message) { }
        }

        public interface IRiskAssessable
        {
            string GetRiskCategory();
        }

        public interface IReportable
        {
            string GenerateReportLine();
        }

        public abstract class FinancialInstrument
        {
            private decimal _quantity;
            private decimal _purchasePrice;
            private string _currency;

            public string InstrumentId { get; set; }
            public string Name { get; set; }
            public DateTime PurchaseDate { get; set; }
            public decimal MarketPrice { get; set; }

            public string Currency
            {
                get => _currency;
                set
                {
                    if (string.IsNullOrWhiteSpace(value) || value.Length != 3)
                        throw new InvalidFinancialDataException("Invalid currency format (must be 3-letter code).");
                    _currency = value.ToUpper();
                }
            }

            public decimal Quantity
            {
                get => _quantity;
                set
                {
                    if (value < 0) throw new InvalidFinancialDataException("Negative quantity not allowed.");
                    _quantity = value;
                }
            }

            public decimal PurchasePrice
            {
                get => _purchasePrice;
                set
                {
                    if (value < 0) throw new InvalidFinancialDataException("Negative price not allowed.");
                    _purchasePrice = value;
                }
            }

            public abstract decimal CalculateCurrentValue();

            public virtual string GetInstrumentSummary()
            {
                return $"{InstrumentId}: {Name} | Qty: {Quantity} | Current Value: {CalculateCurrentValue():C}";
            }
        }

        public class Equity : FinancialInstrument, IRiskAssessable, IReportable
        {
            public override decimal CalculateCurrentValue() => Quantity * MarketPrice;
            public string GetRiskCategory() => "High";
            public string GenerateReportLine() => $"[EQUITY] {GetInstrumentSummary()} | Risk: {GetRiskCategory()}";
        }

        public class Bond : FinancialInstrument, IRiskAssessable, IReportable
        {
            public override decimal CalculateCurrentValue() => Quantity * MarketPrice;
            public string GetRiskCategory() => "Low";
            public string GenerateReportLine() => $"[BOND] {GetInstrumentSummary()} | Risk: {GetRiskCategory()}";
        }

        public class Transaction
        {
            public string TransactionId { get; set; }
            public string InstrumentId { get; set; }
            public string Type { get; set; }
            public decimal Units { get; set; }
            public DateTime Date { get; set; }
        }

        public class Portfolio
        {
            private List<FinancialInstrument> _instruments = new List<FinancialInstrument>();
            private Dictionary<string, FinancialInstrument> _idLookup = new Dictionary<string, FinancialInstrument>();

            public void AddInstrument(FinancialInstrument instrument)
            {
                if (_idLookup.ContainsKey(instrument.InstrumentId))
                    throw new InvalidFinancialDataException("Duplicate instrument ID.");

                _instruments.Add(instrument);
                _idLookup.Add(instrument.InstrumentId, instrument);
            }

            public decimal GetTotalValue() => _instruments.Sum(x => x.CalculateCurrentValue());

            public void ProcessTransactions(Transaction[] transactionArray)
            {
                List<Transaction> transactionList = transactionArray.ToList();
                foreach (var txn in transactionList)
                {
                    if (!_idLookup.ContainsKey(txn.InstrumentId)) continue;

                    var inst = _idLookup[txn.InstrumentId];
                    if (txn.Type == "Sell" && inst.Quantity < txn.Units)
                        throw new InvalidFinancialDataException("Selling more units than owned.");

                    inst.Quantity += (txn.Type == "Buy" ? txn.Units : -txn.Units);
                }
            }

            public List<FinancialInstrument> GetInstruments() => _instruments;
        }

        public class ReportGenerator
        {
            public void ExportToFile(Portfolio portfolio)
            {
                string path = $"PortfolioReport_{DateTime.Now:yyyyMMdd}.txt";
                try
                {
                    using (StreamWriter writer = new StreamWriter(path))
                    {
                        writer.WriteLine("===== FINANCIAL PORTFOLIO REPORT =====");
                        foreach (var inst in portfolio.GetInstruments())
                        {
                            writer.WriteLine(inst.GetInstrumentSummary());
                        }
                        writer.WriteLine($"Total Portfolio Value: {portfolio.GetTotalValue():C}");
                        writer.WriteLine($"Generated at: {DateTime.Now}");
                    }
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"File permission or write error: {ex.Message}");
                }
            }

            public void DisplayConsoleSummary(Portfolio portfolio)
            {
                Console.WriteLine("===== PORTFOLIO SUMMARY =====");
                var groups = portfolio.GetInstruments().GroupBy(i => i.GetType().Name);

                foreach (var group in groups)
                {
                    decimal totalInvest = group.Sum(i => i.PurchasePrice * i.Quantity);
                    decimal currentVal = group.Sum(i => i.CalculateCurrentValue());

                    Console.WriteLine($"Instrument Type: {group.Key}");
                    Console.WriteLine($"Total Investment: {totalInvest:C}");
                    Console.WriteLine($"Current Value: {currentVal:C}");
                    Console.WriteLine($"Profit/Loss: {(currentVal - totalInvest):C}");
                    Console.WriteLine("------------------------------");
                }

                var riskDist = portfolio.GetInstruments()
                    .OfType<IRiskAssessable>()
                    .GroupBy(r => r.GetRiskCategory())
                    .Select(g => $"{g.Key}: {g.Count()}");

                Console.WriteLine("Risk Distribution:");
                foreach (var dist in riskDist) Console.WriteLine(dist);
            }
        }

        class Program
        {
            public static void Main(string[] args)
            
            
            {
                try
                {
                    Portfolio myPortfolio = new Portfolio();
                    ReportGenerator reporter = new ReportGenerator();

                    string csvData = "EQ001,Equity,INFY,INR,100,1500,1650";
                    string[] p = csvData.Split(',');

                    FinancialInstrument instrument = p[1] switch
                    {
                        "Equity" => new Equity(),
                        "Bond" => new Bond(),
                        _ => throw new InvalidFinancialDataException("Invalid CSV format")
                    };

                    instrument.InstrumentId = p[0];
                    instrument.Name = p[2];
                    instrument.Currency = p[3];
                    instrument.Quantity = decimal.Parse(p[4]);
                    instrument.PurchasePrice = decimal.Parse(p[5]);
                    instrument.MarketPrice = decimal.Parse(p[6]);
                    instrument.PurchaseDate = DateTime.Now;

                    myPortfolio.AddInstrument(instrument);

                    Transaction[] txns = {
                    new Transaction { InstrumentId = "EQ001", Type = "Buy", Units = 50, TransactionId = "TXN101" }
                };
                    myPortfolio.ProcessTransactions(txns);

                    reporter.DisplayConsoleSummary(myPortfolio);
                    reporter.ExportToFile(myPortfolio);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}

