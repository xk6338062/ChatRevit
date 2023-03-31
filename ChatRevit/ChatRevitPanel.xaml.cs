using CommunityToolkit.Mvvm.DependencyInjection;
using OpenAI.GPT3;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChatRevit
{
    /// <summary>
    /// ChatRevit.xaml 的交互逻辑
    /// </summary>
    public partial class ChatRevitPanel : Page
    {
        public ChatRevitPanel()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetService<ChatRevitViewModel>();
        }
    }
}
