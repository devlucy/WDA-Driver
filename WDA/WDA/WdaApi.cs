using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace WDA{
 public class WdaApi
{

public readonly string PageSource              ;
public readonly string Screenshot              ;
public readonly string WindowSize              ;
public readonly string LaunchApp               ;
public readonly string TerminateApp            ;
public readonly string State                   ;
public readonly string WithoutReference        ;
public readonly string HideKeyboard            ;
public readonly string Scroll                  ;
public readonly string DragFromToForDuration   ;
public readonly string Tap                     ;
public readonly string DoubleTap               ;
public readonly string Orientation             ;
public readonly string FindElements            ;
public readonly string FindElement             ;
public readonly string ElementRef              ;
public readonly string Home                    ;
public readonly string deactivate              ;
private const string session= "session/"       ;
    public WdaApi(string sessionId) {

     PageSource                = session + sessionId + "/source";
     Screenshot                = "screenshot";
     WindowSize                = session + sessionId + "/window/size";
     LaunchApp                 = session + sessionId + "/wda/apps/launch";
     TerminateApp              = session + sessionId + "/wda/apps/terminate";
     State                     = session + sessionId + "/wda/apps/state";
     WithoutReference          = session + sessionId + "/wda/keys";
     HideKeyboard              = session + sessionId + "/wda/keyboard/dismiss";
     Scroll                    = session + sessionId + "/wda/scroll";
     DragFromToForDuration     = session + sessionId + "/wda/dragfromtoforduration";
     Tap                       = session + sessionId + "/wda/tap/0";
     DoubleTap                 = session + sessionId + "/wda/doubleTap";
     Orientation               = session + sessionId + "/orientation";
     FindElements              = session + sessionId + "/elements";
     FindElement               = session + sessionId + "/element";
     ElementRef                = session + sessionId + "/element/";
     Home                      = "wda/homescreen";
     deactivate                = session + sessionId + "/wda/deactivateApp";

        }   
    }
}



