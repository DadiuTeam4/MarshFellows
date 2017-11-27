/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID MENU_FADEVOLUMEDOWN = 854919495U;
        static const AkUniqueID MENU_FADEVOLUMEUP = 2472021264U;
        static const AkUniqueID MUTEALL = 2308878679U;
        static const AkUniqueID PAUSE_MUSIC = 2735935537U;
        static const AkUniqueID PLAY_AMBIENCE = 278617630U;
        static const AkUniqueID PLAY_BEAR_FS = 1832895494U;
        static const AkUniqueID PLAY_DEER_FS = 675749344U;
        static const AkUniqueID PLAY_FALLTREE = 513156325U;
        static const AkUniqueID PLAY_FS_O = 3464671677U;
        static const AkUniqueID PLAY_FS_P = 3464671650U;
        static const AkUniqueID PLAY_GG_AMBIENCE_WATER = 3361606695U;
        static const AkUniqueID PLAY_GG_FSD_1 = 1046603870U;
        static const AkUniqueID PLAY_GG_FSD_2 = 1046603869U;
        static const AkUniqueID PLAY_GG_FSD_3 = 1046603868U;
        static const AkUniqueID PLAY_GG_FSD_4_1 = 1855550475U;
        static const AkUniqueID PLAY_GG_FSD_CHOIR = 4018210138U;
        static const AkUniqueID PLAY_GG_FSD_SHAMAN_DRUM = 2322861418U;
        static const AkUniqueID PLAY_GG_MENU_CLICK = 3668079463U;
        static const AkUniqueID PLAY_GG_SD_BONFIRE_1 = 564891350U;
        static const AkUniqueID PLAY_GG_SD_CHOIR = 53999030U;
        static const AkUniqueID PLAY_GG_SD_MUD_SINK = 1993353133U;
        static const AkUniqueID PLAY_GG_SD_SHAMAN_DRUM_1 = 3932030894U;
        static const AkUniqueID PLAY_GG_SD_SHAMANTRANSFORMATION = 459538816U;
        static const AkUniqueID PLAY_GG_SD_SINK_1 = 316087560U;
        static const AkUniqueID PLAY_GG_SD_SINK_PH = 4053997955U;
        static const AkUniqueID PLAY_GG_SD_STONE_FALL = 4125224216U;
        static const AkUniqueID PLAY_GG_SD_SWIPE_1 = 3603427629U;
        static const AkUniqueID PLAY_GG_SD_TREE_FALL = 4005720611U;
        static const AkUniqueID PLAY_IMPACTEARTHROCK = 1419803467U;
        static const AkUniqueID PLAY_MUSIC_01 = 3709355747U;
        static const AkUniqueID RESUME_MUSIC = 2940177080U;
        static const AkUniqueID STOP_ALL = 452547817U;
        static const AkUniqueID STOP_GG_SD_SINK_1 = 3148331554U;
        static const AkUniqueID STOP_GG_SD_WIND = 104387825U;
        static const AkUniqueID UNMUTEALL = 3340787584U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace AMBIENCE
        {
            static const AkUniqueID GROUP = 85412153U;

            namespace STATE
            {
                static const AkUniqueID BETWEENFORREST = 2244990470U;
                static const AkUniqueID BYWATER = 1689379159U;
                static const AkUniqueID FORREST = 760316600U;
                static const AkUniqueID MEDIUMOPEN = 2581413256U;
                static const AkUniqueID OPENFEW = 2015800441U;
            } // namespace STATE
        } // namespace AMBIENCE

        namespace MUSIC
        {
            static const AkUniqueID GROUP = 3991942870U;

            namespace STATE
            {
                static const AkUniqueID CROSSROAD = 1606235185U;
                static const AkUniqueID INTRO = 1125500713U;
                static const AkUniqueID O = 84696432U;
                static const AkUniqueID P = 84696431U;
                static const AkUniqueID RITUAL = 886825768U;
            } // namespace STATE
        } // namespace MUSIC

    } // namespace STATES

    namespace SWITCHES
    {
        namespace FS_SWITCH
        {
            static const AkUniqueID GROUP = 210744043U;

            namespace SWITCH
            {
                static const AkUniqueID FS_FORREST = 1704637610U;
                static const AkUniqueID FS_ICE = 193148416U;
                static const AkUniqueID FS_SWAMP = 2790481301U;
            } // namespace SWITCH
        } // namespace FS_SWITCH

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID MUSIC_VOLUME = 1006694123U;
        static const AkUniqueID SFX_VOLUME = 1564184899U;
        static const AkUniqueID SIDECHAIN = 1883033791U;
        static const AkUniqueID SS_AIR_FEAR = 1351367891U;
        static const AkUniqueID SS_AIR_FREEFALL = 3002758120U;
        static const AkUniqueID SS_AIR_FURY = 1029930033U;
        static const AkUniqueID SS_AIR_MONTH = 2648548617U;
        static const AkUniqueID SS_AIR_PRESENCE = 3847924954U;
        static const AkUniqueID SS_AIR_RPM = 822163944U;
        static const AkUniqueID SS_AIR_SIZE = 3074696722U;
        static const AkUniqueID SS_AIR_STORM = 3715662592U;
        static const AkUniqueID SS_AIR_TIMEOFDAY = 3203397129U;
        static const AkUniqueID SS_AIR_TURBULENCE = 4160247818U;
        static const AkUniqueID SWIPEPOWER = 1248482656U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID SOUNDBANK1 = 1647770721U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID AMBIENCE = 85412153U;
        static const AkUniqueID AUX = 983310469U;
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MASTER_SECONDARY_BUS = 805203703U;
        static const AkUniqueID MECHANICS = 2566767142U;
        static const AkUniqueID MUSIC = 3991942870U;
        static const AkUniqueID O_P = 2004479666U;
        static const AkUniqueID SD = 1584861536U;
        static const AkUniqueID SFX = 393239870U;
    } // namespace BUSSES

    namespace AUX_BUSSES
    {
        static const AkUniqueID DELAY = 357718954U;
        static const AkUniqueID EVIL = 4254973567U;
        static const AkUniqueID OUTDOORDELAY = 642728512U;
        static const AkUniqueID REVERB = 348963605U;
    } // namespace AUX_BUSSES

}// namespace AK

#endif // __WWISE_IDS_H__
