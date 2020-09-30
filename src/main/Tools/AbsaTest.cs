using System;
using OpenQA.Selenium;
using AbsaAutomation.src.main.Tools;
using System.Linq;
using AbsaAutomation.src.main.Core;
using AbsaAutomation.src.tests;
using AbsaAutomation.src.main.Pages;
using NUnit.Framework;
using System.Collections.Generic;
using Ninject;
using Ninject.Modules;
using NUnit.Framework.Internal;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal.Builders;

namespace AbsaAutomation.src.main.Tools
{ 
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class AbsaTestAttribute : Attribute, ITestBuilder
	{
		private static IKernel Kernal { get; } = new StandardKernel(new MyModule());

		public IEnumerable<TestMethod> BuildFrom(IMethodInfo method, Test suite)
        {
			var arguements = method.MethodInfo.GetParameters().Select(p => Kernal.Get(p.ParameterType)).Cast<object>().ToArray();

			yield return new NUnitTestCaseBuilder().BuildTestMethod(method, suite, new TestCaseParameters(arguements));
        }
	}

	public class MyModule : NinjectModule
    {
        public override void Load()
        {
			Bind<AbsaBase>().ToSelf();

        }
    }
}
