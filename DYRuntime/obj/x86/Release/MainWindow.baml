   M S B A M L   `   `   `  ����   	DYRuntime    DYRuntime.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$  东雅软件运行时��$	��345��$	��330�� � 
ResizeMode$ CanMinimize=� � WindowStartupLocation$ CenterScreen=����	#FF333333.��� .+��� $
��Left=�$
��Wrap=�$��	内网IP:��$	��Top=�$��26.13,14.999,0,0q�
��������� -    ��Name$ tb_ip�� �� TextWrapping$ NoWrap=�$	��Top=�$��67.89,11.998,0,0q�$
��Left=�$	��175���� $
��Left=�$
��Wrap=�$��端口:��$	��Top=�$��37.13,60.67,0,0q�
��������� -   $ tb_port��$ NoWrap=�$��80��$	��Top=�$��67.89,57.67,0,0q�$
��Left=�$��50���� -    ��Name$ 	bt_启动��$��启动服务��$	��Top=�$��67.89,158.179,0,0q�$��40��$
��Left=�$	��175���� -   $ bt_iisInstall��$��安装运行依赖组件��$��25��$��67.89,202.179,0,0q�$	��Top=�$
��Left=�$	��175���� $
��Left=�$
��Wrap=�$<��6防火墙开放端口可避免内网无法访问错误��$	��Top=�$��67.89,83.506,0,0q�
��������� $
��Left=�$
��Wrap=�$��程序目录:��$	��Top=�$��13,106.342,0,0q�
��������� -   $ tb_path��$ NoWrap=�$	��Top=�$��67.89,103.342,0,0q�$
��Left=�$	��175��$��	D:\dining��{�.  �� 
IsReadOnly . �� -   $ bt_selectPath��$	��...��$��246.89,103.343,0,0q�$��21.837��$	��Top=�$
��Left=�$
��32.5���� -   $��tbk_logo��
�������$
��Left=�$��68.02,260.179,0,0q�$��东雅软件 DYOK.NET��$
��Wrap=�$	��Top=�$��18.667(�$��175.11��d� -    d�Name$ tgb_autorun��$��开机自动运行��$	��Top=�$��67.89,231.179,0,0q�$��25��$
��Left=�$	��175���� -	    ��Name$ cb_mode��$��68.02,129.179,0,0q�$��25��$	��Top=�$
��Left=�$	��175��.@��� 	 �� 
IsSelected	 . $��NET4.0/4.5集成模式��

 � Tag$
 Clr4IntegratedAppPool���� $��NET4.0/4.5经典模式��$
 Clr4ClassicAppPool���� $��NET2.0/3.5集成模式��$
 Clr2IntegratedAppPool���� $��NET2.0/3.5经典模式��$
 Clr2ClassicAppPool���� $��非NET托管程序模式��$
 UnmanagedClassicAppPool���� $
��Left=�$
��Wrap=�$��程序模式:��$��13.13,132.179,0,0q�
�������$	��Top=��� -
    ��Name$ tbk_Ver��$
��Left=�$
��Wrap=�$��67.89,0,0,8q�
�������$	��175�� �� TextAlignment$ Center=�$��Bottom=��� $
��Left=�$
��Wrap=�$@��:网卡设置为固定IP地址可避免路由动态IP错误��$	��Top=�$��68.02,37.834,0,0q�
��������� 
�������$
��Left=�$��
124,61,0,0q�$��
SSL端口:��$
��Wrap=�$	��Top=��� -   $��
tb_sslPort��$
��Left=�$��
177,58,0,0q�$��44300��$ NoWrap=�$	��Top=�$��66��