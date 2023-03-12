using Xunit;
using FluentAssertions;
using trybank;
using System;

namespace trybank.Test;

public class TestThirdReq
{
    [Theory(DisplayName = "Deve devolver o saldo em uma conta logada")]
    [InlineData(70)]
    public void TestCheckBalanceSucess(int balance)
    {
        Trybank instance = new();

        instance.Bank[0, 0] = 70000;
        instance.Bank[0, 1] = 001;
        instance.Bank[0, 2] = 12345;
        instance.Bank[0, 3] = balance;

        instance.CheckBalance().Should().Be(balance);
    }

    [Theory(DisplayName = "Deve lançar uma exceção de usuário não logado")]
    [InlineData(0)]
    public void TestCheckBalanceWithoutLogin(int balance)
    {
        Trybank instance = new();

        instance.CheckBalance();
        instance.Bank[0, 0] = 70000;
        instance.Bank[0, 1] = 001;
        instance.Bank[0, 2] = 12345;
        instance.Bank[0, 3] = balance;

        Action act = () => instance.CheckBalance();
        act.Should().Throw<AccessViolationException>().WithMessage("Usuário não está logado");
    }

    [Theory(DisplayName = "Deve depositar um saldo em uma conta logada")]
    [InlineData(1000000)]
    public void TestDepositSucess(int value)
    {
        Trybank instance = new();

        instance.Deposit(value);
        instance.Bank[0, 3].Should().Be(value);

        instance.Should().Be(value);
    }

    [Theory(DisplayName = "Deve lançar uma exceção de usuário não logado")]
    [InlineData(120)]
    public void TestDepositWithoutLogin(int value)
    {
        Trybank instance = new();

        instance.Deposit(value);

        Action act = () => instance.CheckBalance();
        act.Should().Throw<AccessViolationException>().WithMessage("Usuário não está logado");
    }

    [Theory(DisplayName = "Deve sacar um valor em uma conta logada")]
    [InlineData(70, 70)]
    public void TestWithdrawSucess(int balance, int value)
    {
        Trybank instance = new();

        instance.Withdraw(balance);
        instance.Bank[0, 3] = balance;

        instance.Bank[0, 3].Should().Be(value);
    }

    [Theory(DisplayName = "Deve lançar uma exceção de usuário não logado")]
    [InlineData(0)]
    public void TestWithdrawWithoutLogin(int value)
    {
        Trybank instance = new();

        instance.Withdraw(value);

        Action act = () => instance.Withdraw(value);

        act.Should().Throw<AccessViolationException>().WithMessage("Usuário não está logado");
    }

    [Theory(DisplayName = "Deve lançar uma exceção de usuário não logado")]
    [InlineData(0, 0)]
    public void TestWithdrawWithoutBalance(int balance, int value)
    {
        Trybank instance = new();

        instance.Withdraw(balance);

        instance.Bank[0, 3] = balance;
        instance.Bank[0, 3] = value;
        Action act = () => instance.Withdraw(balance);

        act.Should().Throw<InvalidOperationException>().WithMessage("Saldo insuficiente");
    }
}