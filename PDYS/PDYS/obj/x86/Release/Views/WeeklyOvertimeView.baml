   M S B A M L   `   `   `  ���� K  GPDYS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b541947c23086f5b9!clr-namespace:PDYS.InfraStructurePDYS.InfraStructure  )clr-namespace:PDYS.ModelsPDYS.Models  5clr-namespace:PDYS.UserControlsPDYS.UserControls  #    PDYS.Views.WeeklyOvertimeView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     a ]DevExpress.Xpf.Core.v11.1, Version=11.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Cia:http://schemas.microsoft.com/expression/2010/interactivity  -infra!clr-namespace:PDYS.InfraStructure  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  %modelclr-namespace:PDYS.Models  (ucclr-namespace:PDYS.UserControls  $	��750��$	��400��.���   � ColumnDefinitions  �� $��4* ��� $��6* �.+�� $��0�� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* �  �� $
��Auto ��� $��10 ��� $��* �.+��� $��0��$��0��$��Center=�$��
Mesai Adı��%  DevExpress.Xpf.Editors.TextEdit  $��0��$��2��$��3q�%  DevExpress.Xpf.Editors.BaseEdit   	EditValue �� *Name+ �� Mode$ TwoWay=� �� ValidatesOnDataErrors . �� $��1��$��0��$��Center=�$��Günlük Çalışma Saati��  $��1��$��2��$��60��$��3q�$
��Left=�$��Center=�   Mask$ HH:mm��   MaskAllowNullInput .     MaskIgnoreBlank .     MaskType$ DateTime=�	   MaskUseAsDisplayFormat	 .  �� *
RegularHrs+$ TwoWay=� . �� $��2��$��0��$��Center=�$��Mesai Katsayısı��  $��2��$��2��$��80��$��3q�$
��Left=�$��Center=�$ p0�� .   .  $ Numeric=�	 .  �� *OvertimeFactor+$ TwoWay=� . �� $��3��$��0��$��Center=�$��Ceza Katsayısı��  $��3��$��2��$��80��$��3q�$
��Left=�$��Center=�$ p0�� .   .  $ Numeric=�	 .  �� *MissingFactor+$ TwoWay=� . �� $��4��$��0��$��Center=�$��Resmi Tatil Katsayısı��  $��4��$��2��$��80��$��3q�$
��Left=�$��Center=�$ p0�� .   .  $ Numeric=�	 .  �� *PubHolidayFactor+$ TwoWay=� . �� $��5��$��0��$��Center=�$"��Çalışan İzin Katsayısı��  $��5��$��2��$��80��$��3q�$
��Left=�$��Center=�$ p0�� .   .  $ Numeric=�	 .  �� *HolidayFactor+$ TwoWay=� . �� $��6��$��0��$��Center=�$��Defans Süresi��  $��6��$��2��$��60��$��3q�$
��Left=�$��Center=�$ HH:mm�� .   .  $ DateTime=�	 .  �� *DefenceDuration+$ TwoWay=� . �� $��7��$��0��$��Center=�$��	Gün Sonu��  $��7��$��2��$��60��$��3q�$
��Left=�$��Center=�$ HH:mm�� .   .  $ DateTime=�	 .  �� *DailyCheckPoint+$ TwoWay=� . �� $��8��$��0��$��3��$��0,10,0,0q�$��Stretch=�$��
Açıklama��.��  $��6��$��2��$��3q�$��Stretch=�$��Stretch=�$	��Top=�)  #DevExpress.Xpf.Editors.TextEditBase
   AcceptsReturn
 .    TextWrapping$
 Wrap=�"   VerticalScrollBarVisibility$ Visible=�   Text �� *Description+$ TwoWay=� . �� $��1��$��5,0,0,0q�$��Stretch=�$��Mesai Saati Tanımı��.���  �� $
��Auto ��� $��* �.+��� $��0,0,0,0q�   NormalBorderBrush#���   $��1,1,1,1q�._��� $��0,3,0,3q�$
��Left=� �� Orientation$ 
Horizontal=�.+��� $��75��$��5,0,10,0q�$��Center=�$
��Gün�� �� TextAlignment$ Center=��� $��30��$��0,0,10,0q�$��Center=�$��Tatil��$
 Left=��� $��60��$��0,0,10,0q�$��Center=�$��Mesai Başlangış��$ Center=�$
��Wrap=��� $��60��$��0,0,10,0q�$��Center=�$��Öğlen Yemeği��$ Center=�$
��Wrap=��� $��60��$��0,0,10,0q�$��Center=�$��Öğlen Dönüş��$ Center=�$
��Wrap=��� $��60��$��0,0,10,0q�$��Center=�$��Mesai Bitiş��$ Center=�$
��Wrap=��� $��1������ *OvertimeList+����  �� DataType   PDYS.Models.Overtime"  .R��� $��0,0,0,0q�#���   $��1,0,1,1q�._��� $��0,3,0,3q�$
��Left=�$ 
Horizontal=�.+��� $��75��$��5,0,10,0q�$��Center=����� *	DayName+�� $��30��$��0,0,10,0q�$��Center=�$��Center=� d� 	IsChecked �� *	IsHoliday+$ TwoWay=�  $��60��$��0,3,10,3q�$
��Left=�$��Center=�$ HH:mm�� .   .  $ DateTime=�	 .  �� *Start+$ TwoWay=� . {��� *
IsEditable+$ TwoWay=�  $��60��$��0,3,10,3q�$
��Left=�$��Center=�$ HH:mm�� .   .  $ DateTime=�	 . {��� *
IsEditable+$ TwoWay=� �� *
LunchOut+$ TwoWay=� .   $��60��$��0,3,10,3q�$
��Left=�$��Center=�$ HH:mm�� .   .  $ DateTime=�	 . {��� *
IsEditable+$ TwoWay=� �� *	LunchIn+$ TwoWay=� .   $��60��$��0,3,10,3q�$
��Left=�$��Center=�$ HH:mm�� .   .  $ DateTime=�	 . {��� *
IsEditable+$ TwoWay=� �� *End+$ TwoWay=� . �� $��0,0,5,0q�$��Center=�$��3q����� *CopyCommand+{��� *
IsEditable+$ TwoWay=����� $)��#/PDYS;component/Images/cmd-copy.png��$
��None=��� $��0,0,10,0q�$��Center=�$��3q����� *PasteCommand+{��� *
IsEditable+$ TwoWay=����� $*��$/PDYS;component/Images/cmd-paste.png��$
��None=�