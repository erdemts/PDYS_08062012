   M S B A M L   `   `   `  ���� K  GPDYS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b541947c23086f5b)clr-namespace:PDYS.HelperPDYS.Helper  9!clr-namespace:PDYS.InfraStructurePDYS.InfraStructure  5clr-namespace:PDYS.UserControlsPDYS.UserControls  'clr-namespace:PDYS.Views
PDYS.Views  )    #PDYS.Views.EmployeeInOutScoringView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     a ]DevExpress.Xpf.Core.v11.1, Version=11.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  a ]DevExpress.Xpf.Grid.v11.1, Version=11.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49af bDevExpress.Xpf.Grid.v11.1.Core, Version=11.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxg2http://schemas.devexpress.com/winfx/2008/xaml/grid   (helperclr-namespace:PDYS.Helper   c	 _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Cia:http://schemas.microsoft.com/expression/2010/interactivity 	 -infra!clr-namespace:PDYS.InfraStructure 	 Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006 	 (ucclr-namespace:PDYS.UserControls 	 !vwclr-namespace:PDYS.Views 	 $	��800��.���   � ColumnDefinitions  �� $	��400 ��� $��* �.+�� {��� *
IsEditable+ �� Mode$ TwoWay=�  �� $	��100 ��� $��10 ��� $��* � � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* �.+��� $��0��$��0��$��Center=�$��Personel��$   PDYS.UserControls.ExtendLookup  $��0��$��2��$��3q�{�.     LookupViewModel �� *LookupEmployee+   SelectedDataModel �� *
Employee+$ TwoWay=� �� ValidatesOnDataErrors . �� $��1��$��0��$��Center=�$��Puantaj Tarihi��%  DevExpress.Xpf.Editors.DateEdit  $��1��$��2��$	��150��$��3q�$
��Left=�$��Center=�{�.  %  DevExpress.Xpf.Editors.BaseEdit   	EditValue �� *ScoringDate+$ TwoWay=� . �� $��2��$��0��$��Center=�$��
Mesai Tipi���� $��2��$��2��$��3q�$��Center=� �� Orientation$ 
Horizontal=�.+�	� $��Haftalık Mesai��{�.   d� 	IsChecked �� *IsWeeklyOvertime+$ TwoWay=�	� $��20,0,0,0q�$��Kümulatif Mesai��{�.   �� *IsOutSourceOvertime+$ TwoWay=��� $��3��$��0��$��Center=�$��Kümulatif Mesai��y��� *IsOutSourceOvertime+   VisibilityConverter	 �� 	Converter#	 [    $��3��$��2��$��3q�{�.   �� *LookupOutSourceOvertime+ �� *OutSourceOvertime+$ TwoWay=� . y��� *IsOutSourceOvertime+#	 [  �� $��4��$��0��$��Center=�$��Haftalık Mesai��y��� *IsWeeklyOvertime+#	 [    $��4��$��2��$��3q�{�.   �� *LookupWeeklyOvertime+ �� *WeeklyOvertime+$ TwoWay=� . y��� *IsWeeklyOvertime+#	 [  �� $��5��$��0��$��3��$��0,10,0,0q�$��Stretch=�$��Mesai Giriş  Çıkış��y��� *IsWeeklyOvertime+#	 [  .���   �� $	��100 ��� $��10 ��� $��* � �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��$��0��$��Center=�$��Mesai Başlangıç��    PDYS.Views.WorkingTimeView  $��0��$��2��
 � DataContext
 �� *	StartTime+$ TwoWay=� . �� $��1��$��0��$��Center=�$��Öğlen Çıkış��  $��1��$��2��
 �� *
LunchOut+$ TwoWay=� . �� $��2��$��0��$��Center=�$��Öğlen Giriş��  $��2��$��2��
 �� *	LunchIn+$ TwoWay=� . �� $��3��$��0��$��Center=�$��Mesai Bitiş��  $��3��$��2��
 �� *	EndTime+$ TwoWay=� . �� $��9��$��0��$��3��$��0,10,0,0q�$��Stretch=�$��Tatil Günü��.���� $ 
Horizontal=�.+��� $��20,0,0,0q�$��Center=�$��İzin���� $��5,0,0,0q�$��Center=�{�.   �� *	IsHoliday+$ TwoWay=� . �� $��20,0,0,0q�$��Center=�$��Ücretsiz İzin���� $��5,0,0,0q�$��Center=�{�.   �� *IsNotPaymentHoliday+$ TwoWay=� . �� $��20,0,0,0q�$��Center=�$��Resmi Taitil���� $��5,0,0,0q�$��Center=�{�.   �� *IsPublicHoliday+$ TwoWay=� . �� $��10��$��0��$��3��$��0,10,0,0q�$��Stretch=�$%��Hesaplanan Çalışma Süreleri��.���� .+��� $ 
Horizontal=�.+��� $��0,0,0,0q�$��Center=�$��Toplam Süre��%  DevExpress.Xpf.Editors.TextEdit  $��60��$��3q�$
��Left=�$��Center=�   Mask$ HH:mm��   MaskAllowNullInput .     MaskIgnoreBlank .     MaskType$ DateTime=�   MaskUseAsDisplayFormat .  �� *WorkTotalTime+$ TwoWay=� . {��� *IsOutSourceOvertime+$ TwoWay=��� $��10,0,0,0q�$��Center=�$��Eksik Süre��  $��60��$��3q�$
��Left=�$��Center=�{�.  )  #DevExpress.Xpf.Editors.TextEditBase   Text �� *WorkLessTime+$ TwoWay=� .  �� StringFormat$ {0} dk.���� $��10,0,0,0q�$��Center=�$��	Net Süre��  $��60��$��3q�$
��Left=�$��Center=�{�.  $ HH:mm�� .   .  $ DateTime=� .  �� *WorkNetTime+$ TwoWay=� . �� $��0,10,0,0q�$ 
Horizontal=�.+��� $��0,0,0,0q�$��Center=�$��Tanımlı Mesai Süresi��  $��60��$��3q�$
��Left=�$��Center=�{�.  $ HH:mm�� .   .  $ DateTime=� .  �� *WorkRegularTime+$ TwoWay=� . �� $��55,0,0,0q�$��Center=�$��Eksik/Fazla Mesai��  $��60��$��3q�$
��Left=�$��Center=�{�.   �� *WorkDifference+$ TwoWay=� . $ {0} dk.���� $��11��$��0��$��3��$��0,10,0,0q�$��Stretch=�$��Günlük Hakediş��y��� *IsWeeklyOvertime+#	 [  .���   �� $
��Auto ��� $��10 ��� $��* � �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��1��$��0��$��Center=�$��Günlük Ödeme��  $��1��$��2��$	��120��$��3q�$
��Left=�$��Center=�$��Right=�{�.  $ N2�� .  .  $ Numeric=� .  �� *DataModel.DailyPayment+$ OneWay=� . �� $��2��$��0��$��Center=�$��Kesinti��  $��2��$��2��$	��120��$��3q�$
��Left=�$��Center=�$��Right=�{�.  $ N2�� .  .  $ Numeric=� .  �� *DataModel.DailyDeduction+$ OneWay=� . �� $��3��$��0��$��Center=�$��Fazla Mesai��  $��3��$��2��$	��120��$��3q�$
��Left=�$��Center=�$��Right=�{�.  $ N2�� .  .  $ Numeric=� .  �� *DataModel.DailyExtPayment+$ OneWay=� . �� $��4��$��0��$��Center=�$��
Net Toplam��  $��4��$��2��$	��120��$��3q�$
��Left=�$��Center=�$��Right=�{�.  $ N2�� .  .  $ Numeric=� .  �� *DataModel.DailyNetPayment+$ OneWay=� . � $��1��$��5,0,0,0q� �� $
��Auto ��� $��* � � 	Resources &   System.Windows.Controls.DataGrid  EntityDataGrid. 	 (System.Windows.Interactivity.Interaction   Triggers/	 	 )System.Windows.Interactivity.EventTrigger 	  	EventName.
 	 (System.Windows.Interactivity.TriggerBase 
  Actions.   (PDYS.InfraStructure.ExecuteCommandAction   Command   Columns4  .System.Windows.Controls.DataGridTemplateColumn,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter   Header   
IsReadOnly   CellTemplate �� TextAlignment  TimeConverter0  *System.Windows.Controls.DataGridTextColumn1  +System.Windows.Controls.DataGridBoundColumn   Binding �� ConverterParameter  ParameterConverter��%|  &	       7  7     	  $ MouseDoubleClick��.    �� *MouseDoubleClickCommand+   $	 100 $
 Saat�� .  �� .R��� $��3,0,3,0q�$ Center=����� *	InOutDate+8	     $	 100 $ Hareket Tipi�� .  �� *	InOutType+$ InputOutputType��8	    $	 150 $ Okuyucu Cihaz�� .  �� *ReaderDevice.DisplayName+.+��� $��0��$��Stretch=�$��Center=�$��14(�$
��Bold"�  HeaderFontColor#��[  �� Padding$ 5,3,10,3q�$"��Giriş Çıkış Hareketleri������   
WhiteColor  �� Color#  �  "   PDYS.UserControls.EntityList  $��1��$��0,5,0,0q�!   GridContent#! [ 
 �� *EmployeeInputOutputListVM+