using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenDef {

    /*
        スクリーン定義

        前提: screen_heightとscreen_widthが起動後に直ぐ設定されること

        UIはリファレンスエリアで動作
        横幅固定 ※但し X時は安全マージン追加
        縦がスクリーンサイズにより伸張する
        一定域を超える場合、カメラのview値を修正

        UICanvasCameraSetup.csにて利用中
    */

    public static float screen_height = 0; //Bootプロセスで設定予定
    public static float screen_width  = 0;

    public static float screen_ratio { get { return screen_height / screen_width; }  }

    // 横固定 縦はiPhone/iPad
    public const float reference_width = 640;
    public const float reference_width_w_margin = 700;

    public const float reference_height_max = 1370; //これより大きい時は上下にマスク。左右には安全領域
    public const float reference_height_min = 1136; //iPhone5

    public static bool is_graterthan_reference_min_ratio { get { return screen_ratio >= reference_height_min / reference_width; } }

    private static float reference_simple_height { get
        {
            var d = reference_width / screen_width;
            return screen_height * d;
        } }
    
    public static bool is_reference_simple_hight_graterthan_max { get { return  reference_simple_height > reference_height_max; } }

    public static float reference_height_fix { get {
            if (is_reference_simple_hight_graterthan_max) //iPhoneX対策
            {
                var d = reference_width_w_margin / reference_width;
                return reference_height_max * d;
            }
            else
            { 
                return Mathf.Clamp(reference_simple_height, reference_height_min, reference_height_max);
            }
        } }

    public static float reference_width_fix { get {
            if (is_graterthan_reference_min_ratio)
            {
                if (is_reference_simple_hight_graterthan_max) // iphone X
                {
                    return reference_width_w_margin; //安全領域追加
                }
                else
                { 
                    return reference_width;
                }
            }
            else
            {
                var d = reference_height_min / screen_height;
                return screen_width * d;
            }
        } }


    private static bool is_over_reference_height { get {  return (reference_simple_height > reference_height_max);  } }

    #region Camera View　iPhoneX と Androidの想定外縦長対策
    public static float view_y { get {
            if (!is_over_reference_height) return 0;
            var diff = (reference_simple_height - reference_height_max);
            return (diff / reference_simple_height) * 0.5f;
        } }
    public static float view_height { get
        {
            if (!is_over_reference_height) return 1;
            return 1.0f - view_y * 2;
        } }
    public static float orthographicSize { get {
            return reference_height_fix * 0.5f;

        } }
    #endregion
}
