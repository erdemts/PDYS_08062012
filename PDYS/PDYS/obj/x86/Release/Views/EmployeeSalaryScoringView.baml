   M S B A M L   `   `   `  ���� K  GPDYS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b541947c23086f5b)clr-namespace:PDYS.HelperPDYS.Helper  9!clr-namespace:PDYS.InfraStructurePDYS.InfraStructure  5clr-namespace:PDYS.UserControlsPDYS.UserControls  *    $PDYS.Views.EmployeeSalaryScoringView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     a ]DevExpress.Xpf.Core.v11.1, Version=11.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  a ]DevExpress.Xpf.Grid.v11.1, Version=11.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af bDevExpress.Xpf.Grid.v11.1.Core, Version=11.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   (helperclr-namespace:PDYS.Helper   c	 _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Cia:http://schemas.microsoft.com/expression/2010/interactivity 	 -infra!clr-namespace:PDYS.InfraStructure 	 Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	 (ucclr-namespace:PDYS.UserControls 	 $	��850��$	��300��.���   � ColumnDefinitions  �� $	��300 ��� $��* �.+�� $��0��{�.   � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �  �� $	��100 ��� $��10 ��� $��* �.+��� $��0��$��0��$��Center=�$��Personel��$   PDYS.UserControls.ExtendLookup  $��0��$��2��$��3q�   LookupViewModel �� *LookupEmployee+   SelectedDataModel �� *
Employee+ �� Mode$ TwoWay=� �� ValidatesOnDataErrors . �� $��1��$��0��$��Center=�$��Maaş Dönemi��%  DevExpress.Xpf.Editors.TextEdit  $��1��$��2��$	��100��$��3q�$
��Left=�$��Center=�%  DevExpress.Xpf.Editors.BaseEdit   	EditValue �� *DataModel.PeriodPreview+$ OneWay=��� $��2��$��0��$��Center=�$��Ay - Hakediş Gün��  $��2��$��2��$��70��$��3q�$
��Left=�$��Center=� �� *DataModel.DayCountPreview+$ OneWay=��� $��3��$��0��$��Center=�$��Tanımlı Maaş��  $��3��$��2��$	��120��$��3q�$
��Left=�$��Center=�$��Right=�   Mask$ N2��   MaskAllowNullInput . 	   MaskIgnoreBlank	 .  
   MaskType$
 Numeric=�   MaskUseAsDisplayFormat .  �� *DataModel.DefinedSalary+$ OneWay=� . �� $��4��$��0��$��Center=�$��Toplam Maaş��  $��4��$��2��$	��120��$��3q�$
��Left=�$��Center=�$��Right=�$ N2�� . 	 .  $
 Numeric=� .  �� *DataModel.TotalPayment+$ OneWay=� . �� $��5��$��0��$��Center=�$��Kesinti Tutarı��  $��5��$��2��$	��120��$��3q�$
��Left=�$��Center=�$��Right=�$ N2�� . 	 .  $
 Numeric=� .  �� *DataModel.Deduction+$ OneWay=� . �� $��6��$��0��$��Center=�$��Fazla Mesai Tutarı��  $��6��$��2��$	��120��$��3q�$
��Left=�$��Center=�$��Right=�$ N2�� . 	 .  $
 Numeric=� .  �� *DataModel.ExtPayment+$ OneWay=� . �� $��7��$��0��$��Center=�$��
Net Toplam��  $��7��$��2��$	��120��$��3q�$
��Left=�$��Center=�$��Right=�$ N2�� . 	 .  $
 Numeric=� .  �� *DataModel.NetPayment+$ OneWay=� . � $��1��$��5,0,0,0q� �� $
��Auto ��� $��* � � 	Resources &   System.Windows.Controls.DataGrid   EntityDataGrid. 	 (System.Windows.Interactivity.Interaction   Triggers/ 	 )System.Windows.Interactivity.EventTrigger   	EventName. 	 (System.Windows.Interactivity.TriggerBase   Actions.   (PDYS.InfraStructure.ExecuteCommandAction   Command   Columns4	  .System.Windows.Controls.DataGridTemplateColumn,
  &System.Windows.Controls.DataGridColumn 
  Width5  /System.Windows.Controls.DataGridLengthConverter 
  Header 
  
IsReadOnly 	  CellTemplate �� TextAlignment  DateConverter �� 	Converter �� Orientation � ToolTip  VisibilityConverter  ParameterConverter �� ConverterParameter  CurrencyConverter��%�  &	        7  7  7  7  7  7  7  7  7       $ MouseDoubleClick��.    �� *MouseDoubleClickCommand+ 	  $	 100 $ Tarih�� .  �� .R��� $��3,0,3,0q�$��Center=�$ Center=����� *ScoringDate+8   	  $	 100 $ Durum�� .  �� .R��� $��5,0,5,0q�$ 
Horizontal=�.+��� $��Center=�$(��"/PDYS;component/Images/invalid.png��$
��None=� �� *ProcessMessage+y��� *	IsError+8  �� $��Center=�$&�� /PDYS;component/Images/valid.png��$
��None=� �� *ProcessMessage+y��� *
IsComplate+8  �� $��Center=�$%��/PDYS;component/Images/away.png��$
��None=� �� *ProcessMessage+y��� *	IsEmpty+8  �� $��5,0,0,0q�$��Center=����� *ProcessState+8  $ ProcessState��	  $ 80 $ Toplam�� .  �� .R��� $��3,0,3,0q�$ Right=����� *DailyPayment+8  	  $ 80 $ Kesinti�� .  �� .R��� $��3,0,3,0q�$ Right=����� *DailyDeduction+8  	  $ 80 $ 
Fazla Mes.�� .  �� .R��� $��3,0,3,0q�$ Right=����� *DailyExtPayment+8  	  $ 80 $ 
Net Toplam�� .  �� .R��� $��3,0,3,0q�$ Right=����� *DailyNetPayment+8  .+��� $��0��$��Stretch=�$��Center=�$��14(�$
��Bold"�  HeaderFontColor#��[  �� Padding$ 5,3,10,3q�$��Puantaj Bilgileri������   
WhiteColor �� Color# �  "   PDYS.UserControls.EntityList  $��1��$��0,5,0,0q�   GridContent# [   � DataContext �� *EmployeeInOutScoringListVM+