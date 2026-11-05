using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

namespace Utility {
    public class Screenshot : MonoBehaviour {

        private void Update() {
            if (Input.GetKeyDown(KeyCode.S)) {
                string timeNow = DateTime.Now.ToString("dd-MMMM-yyyy HHmmss");
                ScreenCapture.CaptureScreenshot("Screenshot " + timeNow + ".png");
                print("Took Screenshot");
            }
        }
    }
}
