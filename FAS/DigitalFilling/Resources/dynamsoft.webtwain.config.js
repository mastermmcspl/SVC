//
// Dynamsoft JavaScript Library for Basic Initiation of Dynamic Web TWAIN
// More info on DWT: http://www.dynamsoft.com/Products/WebTWAIN_Overview.aspx
//
// Copyright 2017, Dynamsoft Corporation 
// Author: Dynamsoft Team
// Version: 12.2
//
/// <reference path="dynamsoft.webtwain.initiate.js" />
var Dynamsoft = Dynamsoft || { WebTwainEnv: {} };

Dynamsoft.WebTwainEnv.AutoLoad = true;
///
//Dynamsoft.WebTwainEnv.Containers = [{ContainerId:'dwtcontrolContainer', Width:270, Height:350}];
Dynamsoft.WebTwainEnv.Containers = [{ ContainerId: 'dwtcontrolContainer', Width: 100, Height: 350 }, { ContainerId: 'dwtcontrolContainerLargeViewer', Width: 290, Height: 350 }, ];

Dynamsoft.WebTwainEnv.ProductKey = '******';

Dynamsoft.WebTwainEnv.Trial = true;

Dynamsoft.WebTwainEnv.Debug = false;

///
Dynamsoft.WebTwainEnv.ProductKey = 'A52DAF0605C4DF27EE61C2EF8C1DA38A6E0C641FDA4EB4DF9123F64E6ACA4448DD6DC4C887154DAAA0DDECF785BEC6C8DD6DC4C887154DAADE971A8FF06A990E6E0C641FDA4EB4DFFEA9215B79F68B716E0C641FDA4EB4DF57E5006620458B986E0C641FDA4EB4DFE7407059C31E38386E0C641FDA4EB4DF4589548883F983D66E0C641FDA4EB4DFF0102CBDE43552BD6E0C641FDA4EB4DF326E2BC4E5E80D326E0C641FDA4EB4DF8202BC99A4D7916A6E0C641FDA4EB4DF3B5FCA0797E024FF6E0C641FDA4EB4DF30C526C4A5D77AAB6E0C641FDA4EB4DF35EA01891753B4106E0C641FDA4EB4DF2BD0CA33F4B67A836E0C641FDA4EB4DFC7B3B5257F05902D6E0C641FDA4EB4DFB865F5A01CD867046E0C641FDA4EB4DF2734F01BABEA630F6E0C641FDA4EB4DFDCC97F5B72EA5D7E6E0C641FDA4EB4DF1B2E76847E85057130010000';
///
Dynamsoft.WebTwainEnv.Trial = true;
///
Dynamsoft.WebTwainEnv.ActiveXInstallWithCAB = false;
///
// Dynamsoft.WebTwainEnv.ResourcesPath = 'Resources';

/// All callbacks are defined in the dynamsoft.webtwain.install.js file, you can customize them.

// Dynamsoft.WebTwainEnv.RegisterEvent('OnWebTwainReady', function(){
// 		// webtwain has been inited
// });

