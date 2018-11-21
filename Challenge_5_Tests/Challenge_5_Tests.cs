﻿using System;
using System.Collections.Generic;
using Challenge_5;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Challenge_5_Tests
{
    [TestClass]
    public class Challenge_5_Tests
    {
        private CustomerRepository _customerRepoTest;
        private Customer _customer;
        private Customer _customerTwo;
        private List<Customer> _customers;
        private CustomerStatus _status;

        [TestInitialize]
        public void Arrange()
        {
            _customerRepoTest = new CustomerRepository();
            _customer = new Customer("Kenn", CustomerStatus.Current, "Thank you for your work with us. We appreciate your loyalty. Here's a coupon.");
            _customerTwo = new Customer("Paul", CustomerStatus.Past, "It's been a long time since we've heard from you, we want you back");
        }

        [TestMethod]
        public void CustomerRepository_AddCustomerToList()
        {
            _customers = _customerRepoTest.GetCustomerList();
            _customerRepoTest.AddCustomerToList(_customer);

            var actual = _customers.Count;
            var expected = 1;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CustomerRepository_GetEmailResponse_ShouldReturnCorrectString()
        {
            _status = CustomerStatus.Current;

            var actual = _customerRepoTest.GetEmailResponse(_status);
            var expected = "Thank you for your work with us. We appreciate your loyalty. Here's a coupon.";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CustomerRepository_RemoveCustomerFromList_CountShouldReduceByOne()
        {
            _customers = _customerRepoTest.GetCustomerList();
            _customerRepoTest.AddCustomerToList(_customer);
            _customerRepoTest.AddCustomerToList(_customerTwo);
            _customerRepoTest.RemoveCustomerFromList(_customerTwo.Name);

            var actual = _customers.Count;
            var expected = 1;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CustomerRepository_YesNoResponse_ShouldReturnTrue()
        {
            string s = "ywecijwoie";
            var actual = _customerRepoTest.YesNoResponse(s);
            var expected = true;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CustomerRepository_YesNoResponse_ShouldReturnFalse()
        {
            string s = "wecijwoie";
            var actual = _customerRepoTest.YesNoResponse(s);
            var expected = false;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CustomerRepository_GetCustomerStatus_ShouldReturnCorrectStatus()
        {
            string s = "Potential";

            var actual = _customerRepoTest.GetCustomerStatus(s);
            var expected = CustomerStatus.Potential;

            Assert.AreEqual(expected, actual);
        }
    }
}