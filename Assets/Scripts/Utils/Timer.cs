using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer {

    private bool repeat;
    private float startTime = -1;
    private float currentTime = 0;
    private float duration = 0;

    public Timer(bool repeat) {
        this.repeat = repeat;
    }

    public void start(float duration) {
        startTime = getTime();
        this.duration = duration;
    }

    public void stop() {
        startTime = -1;
    }

    public bool isComplete() {
        if(!isActive())
            return false;
        bool isDone = checkIfDone();
        if(isDone)
            handleRepeat();
        return isDone;
    }

    public bool isActive() {
        return startTime >= 0;
    }

    private bool checkIfDone() {
        currentTime = getTime();
        return (currentTime - startTime) > duration;
    }

    private void handleRepeat() {
        if(repeat)
            startTime = getTime();
        else
            stop();
    }

    private float getTime() {
		return Time.time * 1000;
	}
}
