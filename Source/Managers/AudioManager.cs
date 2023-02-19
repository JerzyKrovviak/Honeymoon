using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Honeymoon.Managers
{
	public class AudioManager
	{
		public static AudioEngine audioEngine;
		private static WaveBank waveBank;
		public static SoundBank soundBank;

		public static void LoadAudioContent()
		{
			audioEngine = new AudioEngine("Content/Sfx/honeymoonxact.xgs");
			waveBank = new WaveBank(audioEngine, "Content/Sfx/Wave Bank.xwb");
			soundBank = new SoundBank(audioEngine, "Content/Sfx/Sound Bank.xsb");
		}
	}
}
