namespace trybank;

public class Trybank
{
    public bool Logged;
    public int loggedUser;

    //0 -> Número da conta
    //1 -> Agência
    //2 -> Senha
    //3 -> Saldo
    public int[,] Bank;
    public int registeredAccounts;
    private int maxAccounts = 50;
    public Trybank()
    {
        loggedUser = -99;
        registeredAccounts = 0;
        Logged = false;
        Bank = new int[maxAccounts, 4];
    }

    public void RegisterAccount(int number, int agency, int pass)
    {
        try
        {
            for (var i = 0; i < Bank.GetLength(registeredAccounts); i++)
            {
                if (Bank[i,i] == number && Bank[i,i] == agency)
                {
                    throw new ArgumentException("A conta já está sendo usada!");
                }
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

        Bank[registeredAccounts, 0] = number;
        Bank[registeredAccounts, 1] = agency;
        Bank[registeredAccounts, 2] = pass;
        Bank[registeredAccounts, 3] = 0;
        registeredAccounts++;
    }

    public void Login(int number, int agency, int pass)
    {
        try
        {
            if (Logged)
            {
                throw new AccessViolationException("Usuário já está logado");
            }

            for (var i = 0; i < Bank.GetLength(0); i++)
            {
                if (Bank[i,i] != number && Bank[i,i] != agency)
                {
                    throw new ArgumentException("Agência + Conta não encontrada");
                }

                if (Bank[i,i] == number && Bank[i,i] == agency && Bank[i,i] != pass)
                {
                    throw new ArgumentException("Senha incorreta");
                }

                if (Bank[i,i] == number && Bank[i,i] == agency && Bank[i,i] == pass)
                {
                    Logged = true;
                    loggedUser = Array.IndexOf(Bank, i);
                }
            }
        }
        catch (AccessViolationException ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public void Logout()
    {
        try
        {
            if (!Logged)
            {
                throw new AccessViolationException("Usuário não está logado");
            }
        }
        catch (AccessViolationException ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

        Logged = false;
        loggedUser = -99;
    }

    public int CheckBalance()
    {
        int returnValue = 0;
        try
        {
            if (!Logged) throw new AccessViolationException("Usuário não está logado");

            for (var i = 0; i < Bank.GetLength(0); i++)
            {
                if (loggedUser == Bank[i, i])
                {
                    returnValue = Bank[i, i];
                }
            }
        }
        catch (AccessViolationException ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

        return returnValue;
    }

    public void Transfer(int destinationNumber, int destinationAgency, int value)
    {
        throw new NotImplementedException();
    }

    public void Deposit(int value)
    {
        try
        {
            if (!Logged) throw new AccessViolationException("Usuário já está logado");

            for (var i = 0; i < Bank.GetLength(0); i++)
            {
                Bank[i, 3] = value;
            }
        }
        catch (AccessViolationException ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public void Withdraw(int value)
    {
        try
        {
            if (!Logged) throw new AccessViolationException("Usuário já está logado");

            for (var i = 0; i < Bank.GetLength(0); i++)
            {
                Bank[i, 3] = value;
                if (Bank[i, 3] <= 0)
                {
                    throw new InvalidOperationException("Saldo insuficiente");
                }
            }
        }
        catch (AccessViolationException ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}
