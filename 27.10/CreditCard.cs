using System;

namespace HomeworkDelegatesEvents;

public delegate void CardEventHandler(string message);

public class CreditCard
{
    private string _cardNumber;
    private string _ownerName;
    private DateTime _expiryDate;
    private string _pin;
    private decimal _creditLimit;
    private decimal _balance;
    private decimal _targetBalance;

    public event CardEventHandler? OnDeposit;
    public event CardEventHandler? OnWithdraw;
    public event CardEventHandler? OnCreditUsed;
    public event CardEventHandler? OnTargetBalanceReached;
    public event CardEventHandler? OnPinChanged;

    public string CardNumber
    {
        get { return _cardNumber; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Номер картки не може бути порожнім.");
            }
            _cardNumber = value;
        }
    }

    public string OwnerName
    {
        get { return _ownerName; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("ПІБ власника не може бути порожнім.");
            }
            _ownerName = value;
        }
    }

    public DateTime ExpiryDate
    {
        get { return _expiryDate; }
        set { _expiryDate = value; }
    }

    public string Pin
    {
        get { return _pin; }
        private set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length != 4)
            {
                throw new ArgumentException("PIN повинен складатися з 4 символів.");
            }
            _pin = value;
        }
    }

    public decimal CreditLimit
    {
        get { return _creditLimit; }
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Кредитний ліміт не може бути від'ємним.");
            }
            _creditLimit = value;
        }
    }

    public decimal Balance
    {
        get { return _balance; }
        private set { _balance = value; }
    }

    public decimal TargetBalance
    {
        get { return _targetBalance; }
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Цільова сума не може бути від'ємною.");
            }
            _targetBalance = value;
        }
    }

    public CreditCard(string cardNumber, string ownerName, DateTime expiryDate, string pin, decimal creditLimit, decimal balance, decimal targetBalance = 10000)
    {
        _cardNumber = string.Empty;
        _ownerName = string.Empty;
        _pin = string.Empty;

        CardNumber = cardNumber;
        OwnerName = ownerName;
        ExpiryDate = expiryDate;
        Pin = pin;
        CreditLimit = creditLimit;
        Balance = balance;
        TargetBalance = targetBalance;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Сума поповнення має бути більшою за 0.");
        }

        decimal previousBalance = Balance;
        Balance += amount;
        OnDeposit?.Invoke($"Рахунок поповнено на {amount} грн. Поточний баланс: {Balance} грн.");

        if (previousBalance < TargetBalance && Balance >= TargetBalance)
        {
            OnTargetBalanceReached?.Invoke($"Досягнуто заданої суми на рахунку ({TargetBalance} грн). Поточний баланс: {Balance} грн.");
        }
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Сума витрати має бути більшою за 0.");
        }

        if (Balance - amount < -CreditLimit)
        {
            throw new Exception("Недостатньо коштів (перевищено кредитний ліміт).");
        }

        decimal previousBalance = Balance;
        Balance -= amount;
        OnWithdraw?.Invoke($"З рахунку витрачено {amount} грн. Поточний баланс: {Balance} грн.");

        if (previousBalance >= 0 && Balance < 0)
        {
            OnCreditUsed?.Invoke($"Почато використання кредитних коштів! Використано кредиту: {Math.Abs(Balance)} грн.");
        }
    }

    public void ChangePin(string oldPin, string newPin)
    {
        if (oldPin != _pin)
        {
            throw new ArgumentException("Невірний старий PIN.");
        }

        Pin = newPin;
        OnPinChanged?.Invoke("PIN-код успішно змінено.");
    }

    public override string ToString()
    {
        return $"Картка: {CardNumber} | Власник: {OwnerName} | Термін дії: {ExpiryDate:MM/yy} | Баланс: {Balance} грн | Кредитний ліміт: {CreditLimit} грн";
    }
}