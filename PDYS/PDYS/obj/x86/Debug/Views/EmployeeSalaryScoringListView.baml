   M S B A M L   `   `   `  ����K  GPDYS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b541947c23086f5b)clr-namespace:PDYS.HelperPDYS.Helper  5      9!clr-namespace:PDYS.InfraStructurePDYS.InfraStructure  5
      5clr-namespace:PDYS.UserControlsPDYS.UserControls  5      .    (PDYS.Views.EmployeeSalaryScoringListView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      b ^DevExpress.Xpf.Core.v11.2, Version=11.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  5      ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5      b ^DevExpress.Xpf.Grid.v11.2, Version=11.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49ag cDevExpress.Xpf.Grid.v11.2.Core, Version=11.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   5      (helperclr-namespace:PDYS.Helper   5      c	 _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Cia:http://schemas.microsoft.com/expression/2010/interactivity 	 5	      -infra!clr-namespace:PDYS.InfraStructure 	 5
      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	 5      (ucclr-namespace:PDYS.UserControls 	 5      $	��600��5      $	��400��5      &   DevExpress.Xpf.Core.ThemeManager    	ThemeName0  *DevExpress.Xpf.Core.ThemeNameTypeConverter$  DeepBlue 5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0,0,0,0q�6   $��Arama Kriteri��6$   .��5      �  � ColumnDefinitions 5      �� 5      $	��100 �6'   �� 5      �� 5      $��10 �6'   �� 5      $	��100 �6'   �� 5       5!      �� 5"      $
��Auto �6$   .+�5%      �� $��3��6   $��Center=�5&       �� Orientation$ 
Horizontal=�5'      .+�5)      �� $��1��6    $��0��5*       $��Center=�5+       $��Peronel��5,       $   PDYS.UserControls.ExtendLookup  5.      $��1��6&   $��1��5/   &   $	��200��50   &   $��3q�51   &   $
��Left=�52   &      LookupViewModel 53   &   �� *LookupPersonal+   SelectedDataModel 54   &   �� *
Personal+ �� Mode$ TwoWay=� �� ValidatesOnDataErrors . �� 5:      $��30,0,0,0q�6    $
��Left=�5;       $��Center=�5<          DarkFontColor#��[  5=       $��Ay��5>       �� 5@      $	��120��6   $��10,0,0,0q�5A      $
��Left=�5B      $��Center=�5C       �� 
IsEditable .  ��5E      �� *	ListMonth+	 �� SelectedItem	 5F      �� *SelectMonth+$ TwoWay=� . �� 5J      $��30,0,0,0q�6    $
��Left=�5K      $��Center=�5L       #��[  5M       $
��Yıl��5N       �� 5P      $��80��6   $��	10,0,10,0q�5Q      $
��Left=�5R      $��Center=�5S       .  ��5U      �� *
ListYear+	 5V      �� *
SelectYear+$ TwoWay=� . 5\      �� 5_      $��1��6   $��4��5`      $��Right=�5a      $��Center=�5b      $ 
Horizontal=�5c      .+�5e      �� $	��120��6   $��3,3,10,3q�5f      $��Hakediş Hesapla��5h      ��5g      �� *ScoringCommand+5h      �� 5j      $	��100��6   $��3q�5k      $��Arama��5m      ��5l      �� *SearchCommand+5m      5n      5o      5p      � 5r   
   $��1��6   $��0,5,0,0q�6   .+�5t      "   PDYS.UserControls.EntityList    EntityDataGrid
   GridContent#
 �  6    � 	Resources 5v      &   System.Windows.Controls.DataGrid. 	 (System.Windows.Interactivity.Interaction   Triggers/ 	 )System.Windows.Interactivity.EventTrigger   	EventName. 	 (System.Windows.Interactivity.TriggerBase   Actions.	   (PDYS.InfraStructure.ExecuteCommandAction 	  Command   Columns0
  *System.Windows.Controls.DataGridTextColumn,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter   Header   
IsReadOnly1  +System.Windows.Controls.DataGridBoundColumn   Binding4  .System.Windows.Controls.DataGridTemplateColumn   CellTemplate � ToolTip  VisibilityConverter �� 	Converter  ParameterConverter �� ConverterParameter  CenterCellStyle   	CellStyle  RightCellStyle  CurrencyConverter��%;  &	       7  7  7  7  7  7  7  7  7  7  7  7  7  7    5x       5z        5{      $ MouseDoubleClick��6*   . 5|      	   69   �� *MouseDoubleClickCommand+5}       5�      
  5�      $	 200 6-   $ Personel��5�   -    .  5�   -   �� *Employee.DisplayName+5�   -     5�      $	 100 61   $ Durum��5�   1    .  5�      �� 5�   "   .R�5�   &   �� $��5,0,5,0q�61   $ 
Horizontal=�6B   .+�5�   *   �� $��Center=�60   $(��"/PDYS;component/Images/invalid.png��5�   0   $
��None=�5�   0    5�   0   �� *ProcessMessage+y�5�   0   �� *	IsError+8   �� 5�   *   $��Center=�60   $&�� /PDYS;component/Images/valid.png��5�   0   $
��None=�5�   0    5�   0   �� *ProcessMessage+y�5�   0   �� *
IsComplate+8  �� 5�   *   $��Center=�60   $%��/PDYS;component/Images/away.png��5�   0   $
��None=�5�   0    5�   0   �� *ProcessMessage+y�5�   0   �� *	IsEmpty+8  �� 5�   *   $��5,0,0,0q�64   $��Center=�5�   4   ��5�   4   �� *ProcessState+8  $ ProcessState��5�   '   5�   #   5�      
  5�      $ 80 6-   8  $ Gün Sayısı��5�   -    .  5�   -   �� *DayCountPreview+5�   -   
  5�      $	 100 6-   8  $ Maaş Dönemi��5�   -    .  5�   -   �� *PeriodPreview+5�   -   
  5�      $	 120 6-   8  $ Hes. Maaş Tutarı��5�   -    .  5�   -   �� *TotalPayment+8  5�   -   
  5�      $	 120 6-   8  $ Ek Ödeme Tutarı��5�   -    .  5�   -   �� *
ExtPayment+8 	 5�   -   
  5�      $	 120 6-   8 
 $ Kesinti Tutarı��5�   -    .  5�   -   �� *	Deduction+8  5�   -   
  5�      $	 120 6-   8  $ 
Net Toplam��5�   -    .  5�   -   �� *
NetPayment+8  5�   -   5�      5�      5�      5�      5�      