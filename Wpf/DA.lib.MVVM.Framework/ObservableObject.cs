using System.ComponentModel;

namespace DA.lib.MVVM.Framework
{
	public class ObservableObject : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;

		protected void RaisePropertyChangedEvent(string propertyName="") =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
