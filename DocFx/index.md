---
_layout: landing
---

## WpfClipboardMonitor 

WPF Clipboard Monitor is written in pure WPF. It does not use any Forms library. It can be used in a lot of different ways.</para>
		
		
### Getting Started

To get started, add the WpfClipboardMonitor package to your project.

### Examples

#### Example: Clipboard Monitor as member
  
The ClipboardMonitor class can be used as a member of your main window.

[!code-csharp[](../Demo/Demo Share/ImageCatcherMemberShare/MainWindow.xaml.cs)]

<code language="c#" source="..\..\Demo\Demo Share\ImageCatcherMemberShare\MainWindow.xaml.cs" title="MainWindow.xaml.cs"/>

#### Example: Clipboard Monitor as window derived class with event

The ClipboardMonitarWindow class can be used as the base of your MainWindow with event handling.

<code language="xaml" source="..\..\Demo\Demo Share\ImageCatcherEventShare\MainWindow.xaml" title="MainWindow.xaml"/>
<code language="c#" source="..\..\Demo\Demo Share\ImageCatcherEventShare\MainWindow.xaml.cs" title="MainWindow.xaml.cs"/>


#### Example: Clipboard Monitor as window derived class with method override

The ClipboardMonitarWindow class can be used as the base of your MainWindow with a overwritten method.

<code language="xaml" source="..\..\Demo\Demo Share\ImageCatcherOverrideShare\MainWindow.xaml" title="MainWindow.xaml"/>
<code language="c#" source="..\..\Demo\Demo Share\ImageCatcherOverrideShare\MainWindow.xaml.cs" title="MainWindow.xaml.cs"/>


#### Example: Clipboard Monitor as window derived class in a MVVM environment

The ClipboardMonitarWindow class can be used as the base of your MainWindow with a command for MVVM architecture.

<code language="xaml" source="..\..\Demo\Demo Share\ImageCatcherMvvmShare\MainView.xaml" title="MainView.xaml"/>
<code language="c#" source="..\..\Demo\Demo Share\ImageCatcherMvvmShare\MainViewModel.cs" title="MainViewModel.cs"/>
 

## Donate

<markup>
	<a href="https://www.paypal.me/GBassman" target="_blank">
		<img src="https://www.paypalobjects.com/en_US/i/btn/btn_donate_LG.gif" border="0" alt="Donate" />
	</a>
</markup>
	


