   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WpfApp1.ViewsWpfApp1.Views  5      7 clr-namespace:WpfApp1.ViewModelsWpfApp1.ViewModels  68   %    WpfApp1.Views.ParameterDataGrid   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:WpfApp1.Views     5      /vm clr-namespace:WpfApp1.ViewModels     68     � DataContext  5	      3  -WpfApp1.ViewModels.ParameterDataGridViewModel  5
   
   .��5      �  � ColumnDefinitions 5   
   �� 5      $��1* �6   �� 5      $��9* �6    � RowDefinitions 5   
   �� 5      $��2* �6   �� 5      $��5* �6   �� 5      $��5* �6   �� 5      $��5* �6   .+�5   
   �� $��2��6   $	��Top=�6)    �� Orientation$ 
Horizontal=�6A   .+�5      o� $	��Top=�6   .@�5      m� $��文件��6   .@�5      m� $��新建��6   m� 5      $��打开��6   m� 5      $��保存��6   5      m� 5      $��编辑��6   $��Center=�6'   $��Center=�6D   .@�5      m� $��复制��6   m� 5       $��粘贴��6   m� 5!      $��删除��6   5"      5#      �� 5$      $��保存��6   ��6"   Z� ��68   Z� �� 5%      $��Center=�63   $��Center=�6P   ��6   �� *
ErrorMsg+6m   5&      � 5'   
   $��1��6   $��3��6   .+�5(      &   System.Windows.Controls.DataGrid     AutoGenerateColumns$
 True��6=   ��6   ��  �� Path$ Testlist�6Z   5)        5*   
   $��1��6   $��1��6    $��3��60   $ False��6_   ���	#FFEDE0E0��6A   �� *List+   Columns 5+      0  *System.Windows.Controls.DataGridTextColumn  5,      ,  &System.Windows.Controls.DataGridColumn   Header$ 测试项名��6L      
Foreground �	#FF8BD0661  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6%   �� $ TestItemName�
   ElementStyle
 5-      �� 5.       �� 
TargetType" ��6    .�5/      ��  �� Property � �� �� Value
 �����6;   �� 50       � ��$ 20(�69   �� 51       � ��$ 李国夫手写体)�6;   52      54        55      $ 	参数名��6>      
IsReadOnly$
 True��6K   	 6%   �� *Name+6K     56      $ 描述��6E   	 6%   �� *Description+6E     57      $ 数据类型��6C   $
 True��6Q   	 6%   �� $
 Type�6Q     58      $ 	参数值��6?   	 6%   �� *Value+6?   5:      5;      5<      