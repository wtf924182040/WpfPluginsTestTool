   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WpfApp1.ViewsWpfApp1.Views  5      7 clr-namespace:WpfApp1.ViewModelsWpfApp1.ViewModels  5          WpfApp1.Views.Msg   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:WpfApp1.Views     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      /vm clr-namespace:WpfApp1.ViewModels     5        � DataContext  5      %   WpfApp1.ViewModels.MsgViewModel  5   
    J� InputBindings 5      �� 5   
    �� Gesture$ Control+Alt+O��69    �� Command 6   �� *OpenAutoCommand+69   �� 5   
   $ Control+Alt+C��6:    6   �� *CloseAutoCommand+6:   �� 5   
   $ 	Control+C��65    6   �� *CopyCommand+65   �� 5   
   $ 	Control+V��66    6   �� *PasteCommand+66   .��5      �  � RowDefinitions 5   
   �� 5      $
��0.5* �6   �� 5      $��9* �6    � ColumnDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5       .+�5"   
   �� $��0��5#      ��5$      �� *Checkstatus+ �� Mode$ TwoWay=� d� 	IsChecked 5%      �� *" IsLogsChangedPropertyInViewModel+$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� 5&   
   $��1��5'      $��0��5(      $��6��5)         WpfApp1.ViewModels.Helper	   
AutoScroll	 5*      �� *" IsLogsChangedPropertyInViewModel+.��5+      �� 
 �� AcceptsReturn$

 True��5,       �� 
AcceptsTab$
 True��5-       �� AutoWordSelection$
 True��5.       �� TextAlignment$
 Left=�51      ��5/      Z� ��50      �� *
Messages+$ TwoWay=�$ PropertyChanged=�51      52      53      54      