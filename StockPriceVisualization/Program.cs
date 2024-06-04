using System;
using System.Threading.Tasks;
using YahooFinanceApi;
using ScottPlot;

class Program
{
    static async Task Main(string[] args)
    {
        // Set the ticker symbol and date range for the stock data
        string ticker = "APPL"; // Example: Apple Inc.
        var startDate = new DateTime(2000, 1, 1);
        var endDate = DateTime.Now;

        // Fetch historical stock data
        var history = await Yahoo.GetHistoricalAsync(ticker, startDate, endDate, Period.Daily);

        // Extract closing prices and dates
        double[] closingPrices = new double[history.Count];
        double[] dates = new double[history.Count];
        int i = 0;
        foreach (var candle in history)
        {
            closingPrices[i] = (double)candle.Close;
            dates[i] = candle.DateTime.ToOADate();
            i++;
        }

        // Create a new ScottPlot plot
        var plt = new ScottPlot.Plot(600, 400);

        // Plot the closing prices
        plt.AddScatter(dates, closingPrices);
        plt.Title($"{ticker} Stock Prices");
        plt.XAxis.DateTimeFormat(true);
        plt.YAxis.Label("Price in USD");
        plt.XAxis.Label("Date");

        // Save the plot as an image
        string outputPath = @"C:\Users\namra\source\repos\Create\Create\bin\Results\StockPrices.png";
        plt.SaveFig(outputPath);

        Console.WriteLine($"Stock data plotted and saved as {outputPath}");
    }
}
