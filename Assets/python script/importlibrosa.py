import librosa
import math
import json
import random

def beatarray(file_name):
    wait = 20
  
    y, sr = librosa.load(file_name, mono=True)

    o_env = librosa.onset.onset_strength(y=y, sr=sr)
    times = librosa.times_like(o_env, sr=sr)
    # 5 for hard, 20 for easy
    peaks = librosa.util.peak_pick(o_env, pre_max=7, post_max=7, pre_avg=7, post_avg=7, delta=0.5, wait=wait)

    o = o_env.tolist()
    t = times.tolist()
    p = peaks.tolist()

    onsets = []
    beattime = []

    for peak_index in p:
        onsets.append(o[peak_index])
        beattime.append(t[peak_index])

    o_max = max(onsets)
    o_min = min(onsets)
    o_avg = (sum(onsets) / len(onsets)) - 0.5

    lower_section = (o_avg - o_min) / 12
    upper_section = (o_max - o_avg) / 24

    def lowsection(size):
        return o_min + (lower_section * size)

    def highsection(size):
        return o_avg + (upper_section * size)

    beatmap = []

    total_a = 0
    total_b = 0
    total_c = 0
    total_d = 0
    total_e = 0
    total_f = 0

    time = 0

    colors = ["red", "blue"]
    directions = ["up", "down","left","right"]
    beat_data = []

    for idx, o in enumerate(onsets):
        time = round(beattime[idx] * 1000)

    
        
        beat_type = random.choice(colors)
        spawn_side = "left" if beat_type == "red" else "right"
        direction = random.choice(directions)

        if o < lowsection(8):
            total_a += 1
            

            beat_data.append(
                {
                "time": round(time/1000, 3),
                "type": beat_type,
                "spawnSide": spawn_side,
                "direction": direction
                }
            )

        elif lowsection(8) <= o < lowsection(10):
            total_b += 1
           

            beat_data.append(
                {
                "time": round(time/1000, 3),
                "type": beat_type,
                "spawnSide": spawn_side,
                "direction": direction
                }
            )
        elif lowsection(10) <= o < o_avg:
            total_c += 1
            

            beat_data.append(
                {
                "time": round(time/1000, 3),
                "type": beat_type,
                "spawnSide": spawn_side,
                "direction": direction
                }
            )
        elif o_avg <= o < highsection(1):
            total_d += 1

            beat_data.append(
                {
                "time": round(time/1000, 3),
                "type": beat_type,
                "spawnSide": spawn_side,
                "direction": direction
                }
            )
        elif highsection(1) <= o < highsection(3):
            total_e += 1
       

            beat_data.append(
                {
                "time": round(time/1000, 3),
                "type": beat_type,
                "spawnSide": spawn_side,
                "direction": direction
                }
            )
        elif o >= highsection(3):
            total_f += 1
      
            beat_data.append(
                {
                "time": round(time/1000, 3),
                "type": beat_type,
                "spawnSide": spawn_side,
                "direction": direction
                }
            )

    print(f"Total notes: {len(onsets)}")
    print(f"Total A: {total_a}")
    print(f"Total B: {total_b}")
    print(f"Total C: {total_c}")
    print(f"Total D: {total_d}")
    print(f"Total E: {total_e}")
    print(f"Total F: {total_f}")

    duration = librosa.get_duration(y=y, sr=sr)
    ending = (math.ceil(duration) * 1000) + 2500

    output = {
    "beats": beat_data,
    }

    output_path = "26.json"
    with open(output_path, "w") as json_file:
        json.dump(output, json_file, indent=4)
    

beatarray("26.wav")