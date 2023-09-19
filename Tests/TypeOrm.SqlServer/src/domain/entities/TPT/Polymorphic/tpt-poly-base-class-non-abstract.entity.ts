import { TptPoly_RootAbstract } from './tpt-poly-root-abstract.entity';
import { TptPoly_SecondLevel } from './tpt-poly-second-level.entity';
import { Entity, PrimaryGeneratedColumn, Column, ManyToOne } from 'typeorm';

@Entity('tpt_poly_base_class_non_abstract')
export class TptPoly_BaseClassNonAbstract extends TptPoly_RootAbstract {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column()
  baseField: string;

  @Column({ nullable: true })
  polySecondlevelId?: string;

  @ManyToOne(() => TptPoly_SecondLevel, (poly_SecondLevel) => poly_SecondLevel.poly_BaseClassNonAbstracts, { cascade: ['insert', 'update'] })
  poly_SecondLevel?: TptPoly_SecondLevel;

}