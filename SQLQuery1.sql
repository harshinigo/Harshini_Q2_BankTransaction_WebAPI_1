-- Create a stored procedure to calculate the closing balance and copy matching data
CREATE PROCEDURE CalculateAndCopyData
AS
BEGIN
    -- Create a temporary table to store calculated closing balances
    CREATE TABLE #TempClosingBalance (
        AccountNumber INT,
        TransactionDate DATE,
        ClosingBalance DECIMAL(10, 2)
    )

    -- Calculate closing balances for each account on a daily basis
    INSERT INTO #TempClosingBalance (AccountNumber, TransactionDate, ClosingBalance)
    SELECT
        rb.AccountNumber,
        rb.Date,
        SUM(rb.Amount) OVER (PARTITION BY rb.AccountNumber ORDER BY rb.Date) AS ClosingBalance
    FROM RawBankTransaction rb

    -- Copy data from Raw_Bank_Transaction to Bank_Transaction where closing balance matches
    INSERT INTO Bank_Transaction (AccountNumber, Date, Narration, Amount, Balance)
    SELECT
        rb.AccountNumber,
        rb.Date,
        rb.Narration,
        rb.Amount,
        tb.ClosingBalance
    FROM RawBankTransaction rb
    JOIN #TempClosingBalance tb ON rb.AccountNumber = tb.AccountNumber AND rb.Date = tb.TransactionDate
END
