   M S B A M L   `   `   `  ���� K  GPDYS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b541947c23086f5b5clr-namespace:PDYS.UserControlsPDYS.UserControls  *    $PDYS.Report.Views.RptInputOutputView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     a ]DevExpress.Xpf.Core.v11.1, Version=11.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  e aDevExpress.Xpf.Printing.v11.1, Version=11.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aBdxp6http://schemas.devexpress.com/winfx/2008/xaml/printing   *ucclr-namespace:PDYS.UserControls   .���   � RowDefinitions  �� $
��Auto ��� $��* �.+�� $��0,0,0,5q� � ColumnDefinitions �� $��* ��� $
��Auto �.+��� $��1��$	��100��$��5q�$	��Top=�$��Raporu Çalıştır������ *ExecuteReportCommand+9� $��Rapor Parametreleri��.���  �� $	��100 ��� �� $��10 ��� $	��100 ���   �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��1��$��0��$��Center=�$��Peronel��$   PDYS.UserControls.ExtendLookup  $��1��$��1��$	��200��$��3q�$
��Left=�   LookupViewModel �� *LookupPersonal+   SelectedDataModel �� *
Personal+ �� Mode$ TwoWay=� �� ValidatesOnDataErrors . �� $��0��$��0��$��Center=�$��Başlangıç Tarihi��%  DevExpress.Xpf.Editors.DateEdit  $��0��$��1��$	��150��$��3q�$
��Left=�$��Center=�%  DevExpress.Xpf.Editors.BaseEdit   AllowNullInput .    	EditValue �� *	StartDate+$ TwoWay=� . �� $��0��$��3��$��Center=�$��Bitiş Tarihi��  $��0��$��4��$	��150��$��3q�$
��Left=�$��Center=� .  �� *	EndDate+$ TwoWay=� . �� $��1��$��3��$
��Left=�$��Center=�   DarkFontColor#��[  $��Giriş / Çıkış Tipi���� $��1��$��4��$	��150��$��3q�$
��Left=� �� 
IsEditable .  ���� *ListInputOutputType+	 �� SelectedItem	 �� *SelectedInputOutput+$ TwoWay=� . �� $��1��$��0,0,0,0q�#��[  $��1,1,1,1q�
 �� Padding$
 0q�._�-  'DevExpress.Xpf.Printing.DocumentPreview     Model �� *PreviewModel+