/*
 * This sample test class was added when NMock3 was added
 * through NuGet.  It demonstrates how to mock properties,
 * methods, and events.  It is safe to delete if you don't
 * need it. 
 */

using System;
using NMock;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NMockTests
{
	[TestClass]
	public class SampleTest
	{
		//
		// For more samples and tutorials: http://nmock3.codeplex.com
		//

		private MockFactory _factory = new MockFactory();

		[TestCleanup]
		public void Cleanup()
		{
			_factory.VerifyAllExpectationsHaveBeenMet();
			_factory.ClearExpectations();
		}

		[TestMethod]
		public void PropertyTest()
		{
			var mock = _factory.CreateMock<ITest>();
			mock.Expects.One.GetProperty(_ => _.Prop).WillReturn("Hello");
			mock.Expects.One.SetPropertyTo(_ => _.Prop = ", World");

			var controller = new Controller(mock.MockObject);
			Assert.AreEqual("Hello, World", controller.PropActions(", World"));
		}

		[TestMethod]
		public void MethodTest()
		{
			var mock = _factory.CreateMock<ITest>();
			mock.Expects.One.MethodWith(_ => _.Method(1, 2, 3, 4)).WillReturn(new Version(5, 6, 7, 8));

			var controller = new Controller(mock.MockObject);
			var version = controller.GetVersion(1, 2, 3, 4);

			mock.Expects.One.Method(_ => _.Method(null)).With(Is.TypeOf<Version>()).WillReturn("3, 4, 5, 6");

			var result = controller.GetVersion(version);
			Assert.AreEqual("3, 4, 5, 6", result);
		}

		[TestMethod]
		public void EventTest()
		{
			var mock = _factory.CreateMock<ITest>();
			var invoker = mock.Expects.One.EventBinding(_ => _.Event += null);

			var controller = new Controller(mock.MockObject);
			controller.InitEvents();

			Assert.IsNull(controller.Status);
			invoker.Invoke();
			Assert.AreEqual("Event Fired!", controller.Status);

		}

		public interface ITest
		{
			string Prop { get; set; }
			Version Method(int a, int b, int c, int d);
			string Method(Version version);
			event EventHandler Event;
		}
		public class Controller
		{
			public string Status;
			private ITest _test;
			public Controller(ITest test)
			{
				_test = test;
			}

			public string PropActions(string arg1)
			{
				_test.Prop = arg1;
				return _test.Prop + arg1;
			}
			public Version GetVersion(int a, int b, int c, int d)
			{
				return _test.Method(a, b, c, d);
			}
			public string GetVersion(Version version)
			{
				return _test.Method(version).ToString();
			}
			public void InitEvents()
			{
				_test.Event += _test_Event;
			}

			void _test_Event(object sender, EventArgs e)
			{
				Status = "Event Fired!";
			}
		}
	}
}