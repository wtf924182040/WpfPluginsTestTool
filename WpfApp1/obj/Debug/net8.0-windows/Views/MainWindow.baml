   M S B A M L   `   `   `  ����G  CWTF.TestTool, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:WpfApp1.ConverterWpfApp1.Converter  5      [ WSystem.Windows.Forms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms 5      ^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35tOclr-namespace:System.Windows.Forms.Integration;assembly=WindowsFormsIntegration System.Windows.Forms.Integration 5      [ WMaterialDesignColors, Version=3.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aY@clr-namespace:MaterialDesignColors;assembly=MaterialDesignColorsMaterialDesignColors 5      5clr-namespace:WpfApp1.ConverterWpfApp1.Converter  5	      !clr-namespace:WpfApp1WpfApp1  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      -clr-namespace:WpfApp1.ViewsWpfApp1.Views  5      7 clr-namespace:WpfApp1.ViewModelsWpfApp1.ViewModels  5          WpfApp1.MainWindow   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R	 NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml   	  5      2Converclr-namespace:WpfApp1.Converter   	  5      \WinFormControls@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms   	  5      gWinFormHostOclr-namespace:System.Windows.Forms.Integration;assembly=WindowsFormsIntegration   	  5      Pcol@clr-namespace:MaterialDesignColors;assembly=MaterialDesignColors   	  5      .cvclr-namespace:WpfApp1.Converter   	  5	      @d2http://schemas.microsoft.com/expression/blend/2008   	  5
      S
 OHandyControl, Version=3.5.2.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol 
 5      !localclr-namespace:WpfApp1 
 5      _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      R NOxyPlot.Wpf, Version=2.2.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0Y UOxyPlot.Wpf.Shared, Version=2.2.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0 phttp://oxyplot.org/wpf   5      2sys&clr-namespace:System;assembly=mscorlib   5      _ [Hardcodet.NotifyIcon.Wpf, Version=1.1.0.0, Culture=neutral, PublicKeyToken=682384a853a08aad)tb http://www.hardcodet.net/taskbar  5      $vlclr-namespace:WpfApp1.Views  5      )vm clr-namespace:WpfApp1.ViewModels  5        � Title$  ToolsBox V1.0��5      $
��1500��5      $
��1000��5       � Icon$ /Views/tools.ico��5       � WindowStartupLocation$ CenterScreen=�5       � DataContext 5      ,   &WpfApp1.ViewModels.MainWindowViewModel  5   
    � 	Resources 5         ss �� 
TargetType �� Property �� Value  EffectShadow1��%g  &	        7  �� 5   
   " ��6   .�5      ��  � �� �	#FFEEEEEE6+   �� 5        �@J�Effect8   6'   �� 5!       � ��$ Stretch=�64   �� 5"       � ��$ Stretch=�62   �� 5#       � ��$ 5q�6'   �� 5$       � ��
 �����6,   �� 5%       � ��$ 1q�60   �� 5&       � ��$ 18(�6)   5'      5(      .��5)      � ���	#FFEEEEEE � ColumnDefinitions 5*   
   �� 5+      $��3* �6   �� 5,      $��9* �6   	 � RowDefinitions	 5.   
   �� 5/      $
��0.8* �6   �� 50      $	��10* �6   �� 51      $��4* �6   .+�53   
   �� $��1��54      $��1��55      $!��【测试类选择列表】��56      .��57      �� 
���������59      �� *
Typelist+
 �� SelectedItem
 5:      �� *SlType+��5;      �� 5<       �� DataType$ x:Type��6#   .R�5=      A�  A� Columns$ 1��6&   .+�5>      �� ��6(   �� *PluginsName+5?      5@      5B      5C      A� 5D   
   $��2��5E      
�������$ 8��5G      .+�5H      d� $��Stretch=�5I      $��Stretch=�5J      ���	#FFEEEEEE$��
菜单(_M)��5L      #��[  5N       d� 	IsChecked 5M      �� *IsOpen+ �� ElementName$ 
DrawerLeft��5N      �� 5O      $��加载类库��5R      #��[  5S      ��5P      ��  �� Path$ NewlyBuiltCommand���5Q      ��  �� RelativeSource �� *self+5S      �� 5T      $��加载已保存的类��5V      #��[  5W      ��5U      �� $ LoadCommand�5W      �� 5X      $��单次运行��5[      #��[  5\      ��5Y      �� $ SingleRunCommand���5Z      ��  �� *self+5\      �� 5b      $��保存��5e      #��[  5f      ��5c      �� $ SaveCommand���5d      ��  �� *self+5f      �� 5g      $��更新��5j      #��[  5k      ��5h      �� $ UpdateCommand���5i      ��  �� *self+5k      �� 5l      $��删除��5o      #��[  5p      ��5m      �� $ DeleteCommand���5n      ��  �� *self+5p      A� 5q      $ 2��6   ���	#FFEEEEEE$��5,5,5,5q�6=   .+�5r      �� $��Center=�6   $��Center=�69   $��18(�6T   循环测试6h   d� 5s        ToggleButtonSwitch#��[ 64  $��Center=�6`  $��Center=�6}  ��6    �� *CycleOperationCommand+��6J   ��  �� *self+$ 	IsChecked� �� UpdateSourceTrigger$ PropertyChanged=� �� NotifyOnSourceUpdated$
 True�� �� NotifyOnTargetUpdated$
 True�� �� NotifyOnValidationError$
 True��" �� ValidatesOnNotifyDataErrors$
 True��6}  5t      5u      �� 5v   
   $��1��5w      $��1��5x      $!��【测试参数编辑器】��5y      .��5z      ( 
 "HandyControl.Controls.PropertyGrid  $��10q�6      SelectedObject 6*   �� *SlType+5{         WpfApp1.Views.Msg  5}   
   $��2��5~      $��0��5      $��2��5�      $��10q�5�      
�������5�      " 
 HandyControl.Controls.Drawer  5�   
   -   5        $��
DrawerLeft��5�         Dock$
 Left=�5�         ShowMode$
 Push=�5�         Content. 5�      �� $	��300��5�        RegionBrush#���  5�        BorderBrush#���  5�      ._�5�      � 	 5�      �� 5�      $
��Auto �6(   �� 5�      .+�5�      �� $��10,0,0,0q�5�      $
��Left=�5�        TextBlockTitle#��[ 5�      $��Header��5�      �� 5�      $��0��5�      $��Right=�5�        DeleteFillCircleGeometry' 
 !HandyControl.Controls.IconElement   Geometry# [ 5�      $��hc:ControlCommands.Close��5�        PrimaryTextBrush#���  5�        
ButtonIcon#��[ 5�      5�      5�      5�      5�      5�      