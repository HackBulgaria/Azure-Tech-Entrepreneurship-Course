using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace MyNativeApp
{
	public abstract class ViewModelBase : IoCAwareBase, INavigationAware, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public abstract string Title { get; }

		public virtual  void NavigatedTo(params object[] parameters)
		{
		}

		protected void Set<T>(T value, ref T backingField, [CallerMemberName] string propertyName = null)
		{
			if (!EqualityComparer<T>.Default.Equals(value, backingField))
			{
				backingField = value;
				this.RaisePropertyChanged(propertyName);
			}
		}

		protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
		{
			var handler = this.PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}