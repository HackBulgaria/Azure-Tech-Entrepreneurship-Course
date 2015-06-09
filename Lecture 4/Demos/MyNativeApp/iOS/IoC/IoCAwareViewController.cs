using System;
using UIKit;
using System.ComponentModel;
using System.Collections.Generic;

namespace MyNativeApp.iOS
{
	public interface IModeledController
	{
		ViewModelBase Model { get; }
	}

	public abstract class IoCAwareViewController<TModel> : UIViewController, IModeledController where TModel : ViewModelBase
	{
		private readonly IDictionary<string, Action> propertyChangedCallbacks = new Dictionary<string, Action>();

		[Import]
		public TModel ViewModel { get; set; }

		ViewModelBase IModeledController.Model
		{
			get
			{
				return this.ViewModel;
			}
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			this.BindEvents();
		}

		public override void ViewDidDisappear(bool animated)
		{
			base.ViewDidDisappear(animated);

			this.UnbindEvents();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.Title = this.ViewModel.Title;
		}

		protected IoCAwareViewController(IntPtr handle) : base(handle)
		{
			this.ResolveDependencies();
		}

		protected virtual void BindEvents()
		{
			this.ViewModel.PropertyChanged += this.OnPropertyChanged;

			foreach (var action in this.propertyChangedCallbacks.Values)
			{
				action();
			}
		}

		protected virtual void UnbindEvents()
		{
			this.ViewModel.PropertyChanged -= this.OnPropertyChanged;
		}

		protected void Bind(string propertyName, Action action)
		{
			this.propertyChangedCallbacks[propertyName] = action;
			action();
		}

		private void OnPropertyChanged (object sender, PropertyChangedEventArgs e)
		{
			Action action;
			if (this.propertyChangedCallbacks.TryGetValue(e.PropertyName, out action))
			{
				action();
			}
		}
	}
}