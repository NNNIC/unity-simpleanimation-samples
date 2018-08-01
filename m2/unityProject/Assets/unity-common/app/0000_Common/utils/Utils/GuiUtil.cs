using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GuiUtil {

    public static Vector2 ConvertInputPosition(Vector3 posφ, float? screenHeight=null)
    {
        float sh = screenHeight==null ? (float)Screen.height : (float)screenHeight; //Editor時適切な場所で呼ばないとScreen.Heightは不正値を返すため
        return new Vector2(posφ.x, sh - posφ.y);
    }

    
    public static float reference_screen_height = 1136;
    public static float reference_screen_width  = 640;
    /// <summary>
    /// ※OnGUIにてのみ利用化
    /// アプリの参照スクリーンサイズのワールド座標系からＧＵＩ座標系のＲｅｃｔを求める。
    /// 
    /// ※高さのみ合わせた状態のみ想定
    /// 
    /// </summary>
    public static Rect GetButtonRect(Vector3 reference_worldpos, float button_width, float button_height)
    {
        var x = reference_worldpos.x;
        var y = reference_worldpos.y;

        var ratio = (float)Screen.height / reference_screen_height;
        var pref_width = Screen.width / ratio;
        var nx = pref_width * 0.5f + x;
        var ny = reference_screen_height * 0.5f - y;

        var rnx = nx * ratio;
        var rny = ny * ratio;
            
        var rbut_w = button_width * ratio;
        var rbut_h = button_height * ratio;

        return new Rect(rnx,rny,rbut_w,rbut_h);
    }

    /// <summary>
    /// ※OnGUIにてのみ利用化
    ///   ４方向ボタンのRectを返す
    /// 
    /// </summary>
    public static Rect[] Get4DirButtons(Vector3 reference_worldpos, float button_width, float button_height)
    {
        //up button
        var Rect_up    = GetButtonRect(reference_worldpos + Vector3.up * button_height, button_width, button_height);

        //right button
        var Rect_right = GetButtonRect(reference_worldpos + Vector3.right * button_width, button_width, button_height);

        //down button
        var Rect_down  = GetButtonRect(reference_worldpos + Vector3.down * button_height, button_width, button_height);

        //left button
        var Rect_left  = GetButtonRect(reference_worldpos + Vector3.left * button_width, button_width, button_height);

        //center button
        var Rect_center= GetButtonRect(reference_worldpos , button_width, button_height);

        return new Rect[5]{ Rect_up, Rect_right, Rect_down, Rect_left, Rect_center  };
    }


}
