using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace XamarinSample
{
    public class DrawView : ImageView
    {
        public DrawView(Context context)
            : base(context)
        {
            SetDrawDefaults();
        }

        public DrawView(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            SetDrawDefaults();
        }

        public DrawView(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
            SetDrawDefaults();
        }

        private void SetDrawDefaults()
        {
            CurrentLineColor = Color.OrangeRed;
            PenWidth = 5.0f;

            DrawPath = new Path();
            DrawPaint = new Paint
            {
                Color = CurrentLineColor,
                AntiAlias = true,
                StrokeWidth = PenWidth
            };

            DrawPaint.SetStyle(Paint.Style.Stroke);
            DrawPaint.StrokeJoin = Paint.Join.Round;
            DrawPaint.StrokeCap = Paint.Cap.Round;

            CanvasPaint = new Paint
            {
                Dither = true
            };
        }

        public Color CurrentLineColor { get; set; }
        public float PenWidth { get; set; }

        private Path DrawPath;
        private Paint DrawPaint;
        private Paint CanvasPaint;
        private Canvas DrawCanvas;
        private Bitmap CanvasBitmap;

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);
            CanvasBitmap = Bitmap.CreateBitmap(w, h, Bitmap.Config.Argb8888);
            DrawCanvas = new Canvas(CanvasBitmap);
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            DrawPaint.Color = CurrentLineColor;
            canvas.DrawBitmap(CanvasBitmap, 0, 0, CanvasPaint);
            canvas.DrawPath(DrawPath, DrawPaint);
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            var touchX = e.GetX();
            var touchY = e.GetY();

            switch (e.Action)
            {
                case MotionEventActions.Down:
                    DrawPath.MoveTo(touchX, touchY);
                    break;
                case MotionEventActions.Move:
                    DrawPath.LineTo(touchX, touchY);
                    break;
                case MotionEventActions.Up:
                    DrawCanvas.DrawPath(DrawPath, DrawPaint);
                    DrawPath.Reset();
                    break;
                default:
                    return false;
            }

            Invalidate();

            return true;
        }
    }
}