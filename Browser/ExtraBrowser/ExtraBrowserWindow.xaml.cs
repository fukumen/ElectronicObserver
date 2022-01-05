﻿using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using Windows.Graphics.Imaging;

namespace Browser.ExtraBrowser;

public partial class ExtraBrowserWindow : Window
{
	public FormBrowserTranslationViewModel FormBrowser { get; }
	private CoreWebView2Frame? gameframe { get; set; }
	private CoreWebView2Frame? kancolleframe { get; set; }
	private string StyleClassID { get; } = Guid.NewGuid().ToString().Substring(0, 8);
	public ExtraBrowserWindow()
	{
		FormBrowser = App.Current.Services.GetService<FormBrowserTranslationViewModel>()!;
		InitializeComponent();
		InitializeAsync();
	}

	private async void InitializeAsync()
	{
		var path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "temp_browser");
		var env = await CoreWebView2Environment.CreateAsync(userDataFolder: path);

		await Browser.EnsureCoreWebView2Async(env);
		//Browser.CoreWebView2.WebResourceRequested += CoreWebView2_WebResourceRequested;
		//Browser.CoreWebView2.AddWebResourceRequestedFilter("*", CoreWebView2WebResourceContext.Script);
		//Browser.CoreWebView2.NavigationCompleted += OnNavigationCompleted;
		//Browser.CoreWebView2.FrameCreated += OnFrameCreated;
		//SetCookie();
		//Browser.CoreWebView2.Navigate("http://www.dmm.com/netgame/social/-/gadgets/=/app_id=854854/");
		Browser.Source = new Uri("http://www.duckduckgo.com");
		txtBoxAddress.Text = "http://www.duckduckgo.com";
	}

	private void OnNavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
	{
		if (e.IsSuccess)
		{
			SetStyleSheet();
		}
	}

	private void OnFrameCreated(object? sender, CoreWebView2FrameCreatedEventArgs e)
	{
		if (e.Frame.Name.Contains(@"game_frame"))
		{
			gameframe = e.Frame;
		}
		if (e.Frame.Name.Contains(@"htmlWrap"))
		{
			kancolleframe = e.Frame;
		}
	}

	private void CoreWebView2_WebResourceRequested(object? sender, CoreWebView2WebResourceRequestedEventArgs e)
	{
		if (e.Request.Uri.Contains(@"gadget_html5"))
		{
			e.Request.Uri = e.Request.Uri.Replace("http://203.104.209.7/gadget_html5/", "https://kcwiki.github.io/cache/gadget_html5/");
		}
		if (e.Request.Uri.Contains("kcs2/resources/bgm"))
		{
			e.Request.Headers.RemoveHeader("Range");
		}
	}

	private void SetCookie()
	{
		var gamesCookies = Browser.CoreWebView2.CookieManager.CreateCookie("ckcy", "1", "games.dmm.com", "/");
		gamesCookies.Expires = DateTime.Now.AddYears(6);
		gamesCookies.IsSecure = true;
		Browser.CoreWebView2.CookieManager.AddOrUpdateCookie(gamesCookies);
		var dmmCookie = Browser.CoreWebView2.CookieManager.CreateCookie("ckcy", "1", ".dmm.com", "/");
		dmmCookie.Expires = DateTime.Now.AddYears(6);
		dmmCookie.IsSecure = true;
		Browser.CoreWebView2.CookieManager.AddOrUpdateCookie(dmmCookie);
		var acccountsCookie = Browser.CoreWebView2.CookieManager.CreateCookie("ckcy", "1", "accounts.dmm.com", "/");
		acccountsCookie.Expires = DateTime.Now.AddYears(6);
		acccountsCookie.IsSecure = true;
		Browser.CoreWebView2.CookieManager.AddOrUpdateCookie(acccountsCookie);
	}

	private void DmmPointsButtonClick(object sender, RoutedEventArgs e)
	{
		Browser.CoreWebView2.Navigate("https://point.dmm.com/choice/pay");
		txtBoxAddress.Text = "https://point.dmm.com/choice/pay";
	}

	private void AkashiListButtonClick(object sender, RoutedEventArgs e)
	{
		Browser.CoreWebView2.Navigate("https://akashi-list.me/");
		txtBoxAddress.Text = "https://akashi-list.me/";
	}


	private void ShowDevToolsMenuItemClick(object sender, RoutedEventArgs e)
	{
		Browser.CoreWebView2.OpenDevToolsWindow();
	}

	private void BackButtonClick(object sender, RoutedEventArgs e)
	{
		if (Browser.CoreWebView2.CanGoBack)
		{
			Browser.CoreWebView2.GoBack();
		}
	}
	private void ForwardButtonClick(object sender, RoutedEventArgs e)
	{
		if (Browser.CoreWebView2.CanGoForward)
		{
			Browser.CoreWebView2.GoForward();
		}
	}
	private void SetStyleSheet()
	{
		if (Browser is { IsInitialized: true })
		{
			if (gameframe != null)
			{
				Browser.ExecuteScriptAsync(String.Format(Properties.Resources.PageScript, StyleClassID));
				gameframe.ExecuteScriptAsync(String.Format(Properties.Resources.FrameScript, StyleClassID));
				gameframe.ExecuteScriptAsync("document.body.style.backgroundColor = \"#000000\";");
				Browser.HorizontalAlignment = HorizontalAlignment.Center;
				Browser.VerticalAlignment = VerticalAlignment.Center;
				Browser.Width = 1200;
				Browser.Height = 720;
			}
		}
	}
	private void BoxAddressKeyDown(object sender, KeyEventArgs e)
	{
		if (e.Key == Key.Enter)
		{
			if (Browser != null && Browser.CoreWebView2 != null)
			{
				var url = txtBoxAddress.Text;
				if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
				{
					Browser?.CoreWebView2.Navigate(url);
				}
				else
				{
					e.Handled = true;
				}
			}
		}
	}
}
