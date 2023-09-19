import { TphPoly_BaseClassNonAbstract } from './tph-poly-base-class-non-abstract.entity';
import { Entity, PrimaryGeneratedColumn, Column, ManyToOne } from 'typeorm';

@Entity('tph_poly_second_level')
export class TphPoly_SecondLevel {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column()
  secondField: string;

  @Column()
  polyBaseclassnonabstractId: string;

  @ManyToOne(() => TphPoly_BaseClassNonAbstract, { cascade: ['insert', 'update'], nullable: false })
  poly_BaseClassNonAbstract: TphPoly_BaseClassNonAbstract;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}