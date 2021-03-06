﻿using AnylineXamarinSDK.iOS;
using CoreGraphics;
using Foundation;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UIKit;

namespace AnylineXamarinApp.iOS
{
    class AnylineIdentificationView : UIView
    {
        UILabel nr;
        UILabel surname;
        UILabel givenNames;
        UILabel code;
        UILabel type;
        UILabel dayOfBirth;
        UILabel expirationDate;
        UILabel sex;
        UILabel line0;
        UILabel line1;
        UILabel line2;

        public AnylineIdentificationView() : base()
        {
            SetupView();
        }

        public AnylineIdentificationView(CGRect frame) : base(frame)
        {
            SetupView();
        }

        private void SetupView()
        {
            //look of the view:

            UIBezierPath shadowPath = UIBezierPath.FromRoundedRect(Bounds, 5.0f);
            Layer.ShadowColor = UIColor.White.CGColor;
            Layer.ShadowPath = shadowPath.CGPath;
            Layer.ShadowOffset = new CGSize(0.0f, 0.0f);
            Layer.ShadowOpacity = 0.7f;
            Layer.ShadowRadius = 50.0f;
            Layer.MasksToBounds = false;
            BackgroundColor = UIColor.Clear;

            UIImageView backgroundView = new UIImageView(UIImage.FromBundle(@"pass"));
            //UIImageView backgroundView = new UIImageView(UIImage.FromFile(@"drawable/pass.png"));

            AddSubview(backgroundView);
            SendSubviewToBack(backgroundView);

            //fields:

            nr = new AL2();
            nr.Text = @"NRNRNRNR";
            nr.Frame = new CGRect(Frame.Size.Width-80,7,80,30);
            AddSubview(nr);

            surname = new AL2();
            surname.Text = @"SURNAME";
            surname.Frame = new CGRect(Frame.Size.Width - 200, 32, 200, 30);
            AddSubview(surname);

            givenNames = new AL2();
            givenNames.Text = @"GIVEN NAMES";
            givenNames.Frame = new CGRect(Frame.Size.Width - 200, 57, 200, 30);
            AddSubview(givenNames);

            code = new AL2();
            code.Text = @"AUT";
            code.Frame = new CGRect(Frame.Size.Width - 180, 7, 200, 30);
            AddSubview(code);

            type = new AL2();
            type.Frame = new CGRect(Frame.Size.Width - 200, 7, 200, 30);
            AddSubview(type);

            dayOfBirth = new AL2();
            dayOfBirth.Text = @"19851242"; 
            dayOfBirth.Frame = new CGRect(Frame.Size.Width - 200, 82, 80, 30);
            AddSubview(dayOfBirth);

            expirationDate = new AL2();
            expirationDate.Text = @"20200121";
            expirationDate.Frame = new CGRect(Frame.Size.Width - 200, 107, 80, 30);
            AddSubview(expirationDate);

            sex = new AL2();
            sex.Text = @"M";
            sex.Frame = new CGRect(Frame.Size.Width - 200, 132, 200, 30);
            AddSubview(sex);

            line0 = new AL1();
            line0.Text = @"P<AUTGASSER<<MATTHIAS<<<<<<<<<<<<<<<<<<<<<<<";
            line0.Frame = new CGRect(6, 165, 300, 20);
            AddSubview(line0);

            line1 = new AL1();
            line1.Text = @"P2004908<6AUT8S07125M1706103<<<<<<<<<<<<<<<4";
            line1.Frame = new CGRect(6, 178, 300, 20);
            AddSubview(line1);

            line2 = new AL1();
            line2.Text = @"P2004908<6AUT8S07125M1706103<<<<<<<<<<<<<<<4";
            line2.Frame = new CGRect(6, 191, 300, 20);
            AddSubview(line2);

        }
        
        class AL1 : UILabel
        {
            public AL1()
            {
                Frame = CGRect.Empty;
                Font = UIFont.FromName(@"CourierNewPSMT", 11.0f);
            }
        };

        class AL2 : UILabel
        {
            public AL2()
            {
                Frame = CGRect.Empty;
                Font = UIFont.FromName(@"CourierNewPS-BoldMT", 12.0f);
            }
        };

        public void UpdateIdentification(ALIdentification aId)
        {
            nr.Text = aId.DocumentNumber;
            surname.Text = aId.SurNames;
            givenNames.Text = aId.GivenNames;
            code.Text = aId.NationalityCountryCode;
            type.Text = aId.DocumentType;
            dayOfBirth.Text = aId.DayOfBirth;
            expirationDate.Text = aId.ExpirationDate;
            sex.Text = aId.Sex;

            if (aId.DocumentType.Equals(@"P"))
            {
                line0.Text = String.Format("{0,-44}",
                    String.Format("P<{0}{1}<<{2}", aId.NationalityCountryCode, aId.SurNames, aId.GivenNames)
                    ).Replace(" ","<");
                
                line1.Text = "";
                
                line2.Text = String.Format("{0,-42}{1,1}{2,1}", String.Format("{0,-9}{1}{2}{3}{4}{5}{6}{7}{8}",
                                aId.DocumentNumber, aId.CheckdigitNumber,
                                aId.IssuingCountryCode,
                                aId.DayOfBirth, aId.CheckdigitDayOfBirth,
                                aId.Sex,
                                aId.ExpirationDate, aId.CheckdigitExpirationDate,
                                aId.PersonalNumber),
                        aId.CheckDigitPersonalNumber, aId.CheckdigitFinal)
                        .Replace(" ", "<");

            }
            else
            {
                line0.Text = String.Format("{0,-30}\n", String.Format("{0}{1,-3}{2,-9}{3}",
                        aId.DocumentType, aId.NationalityCountryCode,
                        aId.DocumentNumber, aId.CheckdigitNumber))
                        .Replace(" ","<");

                line1.Text = String.Format("{0,-29}{1,1}\n", String.Format("{0}{1}{2}{3}{4}{5}",
                                aId.DayOfBirth, aId.CheckdigitDayOfBirth,
                                aId.Sex, aId.ExpirationDate,
                                aId.CheckdigitExpirationDate, aId.IssuingCountryCode),
                        aId.CheckdigitFinal)
                        .Replace(" ","<");

                line2.Text = String.Format("{0,-30}", String.Format("{0}<<{1}",
                        aId.SurNames, aId.GivenNames))
                        .Replace(" ","<");
            }
        }
    }
}
